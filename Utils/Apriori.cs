using System;
using System.Collections.Generic;
using System.Linq;

namespace WebBanSach.Utils
{
    public static class Apriori
    {
        public static List<List<int>> FindFrequentItemsets(List<List<int>> transactions, double minSupport)
        {
            int totalTransactions = transactions.Count;
            var frequentItemsets = new List<List<int>>();
            // Bước 1: Tìm các tập hợp 1 phần tử (1-itemsets)
            var itemCount = new Dictionary<int, int>();
            foreach (var transaction in transactions)
            {
                foreach (var item in transaction)
                {
                    if (itemCount.ContainsKey(item))
                        itemCount[item]++;
                    else
                        itemCount[item] = 1;
                }
            }
            var frequentItems = itemCount
                .Where(kv => (double)kv.Value / totalTransactions >= minSupport)
                .Select(kv => new List<int> { kv.Key })
                .ToList();
            frequentItemsets.AddRange(frequentItems);
            // Bước 2: Tạo các tập hợp lớn hơn (k-itemsets)
            int k = 2;
            var currentItemsets = frequentItems;
            while (currentItemsets.Any())
            {
                // Tạo các tập hợp k phần tử từ các tập hợp (k-1) phần tử
                var candidateItemsets = GenerateCandidates(currentItemsets, k);
                // Đếm tần suất của các tập hợp k phần tử
                var newItemsets = new List<List<int>>();
                foreach (var candidate in candidateItemsets)
                {
                    int count = transactions.Count(t => candidate.All(item => t.Contains(item)));
                    if ((double)count / totalTransactions >= minSupport)
                    {
                        newItemsets.Add(candidate);
                    }
                }
                frequentItemsets.AddRange(newItemsets);
                currentItemsets = newItemsets;
                k++;
            }
            // Chỉ giữ các tập hợp có kích thước >= 2 để gợi ý
            return frequentItemsets.Where(itemset => itemset.Count >= 2).ToList();
        }
        private static List<List<int>> GenerateCandidates(List<List<int>> previousItemsets, int k)
        {
            var candidates = new List<List<int>>();
            var itemsetDict = new HashSet<string>();

            for (int i = 0; i < previousItemsets.Count; i++)
            {
                for (int j = i + 1; j < previousItemsets.Count; j++)
                {
                    var itemset1 = previousItemsets[i];
                    var itemset2 = previousItemsets[j];

                    // So sánh k-2 phần tử đầu
                    bool canCombine = true;
                    for (int m = 0; m < k - 2; m++)
                    {
                        if (itemset1[m] != itemset2[m])
                        {
                            canCombine = false;
                            break;
                        }
                    }

                    if (canCombine)
                    {
                        // Tạo candidate mới
                        var newItemset = itemset1.Take(k - 2).ToList();
                        newItemset.Add(itemset1[k - 2]);
                        newItemset.Add(itemset2[k - 2]);
                        newItemset.Sort();

                        // Cắt tỉa: chỉ giữ candidate nếu tất cả tập con (k-1) phần tử đều tồn tại
                        bool allSubsetsFrequent = true;
                        foreach (var subset in GetSubsets(newItemset, k - 1))
                        {
                            if (!previousItemsets.Any(p => p.SequenceEqual(subset)))
                            {
                                allSubsetsFrequent = false;
                                break;
                            }
                        }

                        // Log kết quả cắt tỉa
                        if (!allSubsetsFrequent)
                        {
                            System.Diagnostics.Debug.WriteLine($"❌ Bị cắt tỉa: [{string.Join(", ", newItemset)}]");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"✅ Ứng viên giữ lại: [{string.Join(", ", newItemset)}]");
                        }

                        // Nếu hợp lệ và chưa tồn tại, thêm vào
                        var key = string.Join(",", newItemset);
                        if (allSubsetsFrequent && !itemsetDict.Contains(key))
                        {
                            itemsetDict.Add(key);
                            candidates.Add(newItemset);
                        }
                    }
                }
            }

            return candidates;
        }


        // Hàm tạo tất cả tập con có độ dài targetSize
        private static List<List<int>> GetSubsets(List<int> itemset, int targetSize)
        {
            var result = new List<List<int>>();
            GenerateSubsetRecursive(itemset, new List<int>(), 0, targetSize, result);
            return result;
        }

        private static void GenerateSubsetRecursive(List<int> input, List<int> current, int index, int targetSize, List<List<int>> result)
        {
            if (current.Count == targetSize)
            {
                result.Add(new List<int>(current));
                return;
            }

            for (int i = index; i < input.Count; i++)
            {
                current.Add(input[i]);
                GenerateSubsetRecursive(input, current, i + 1, targetSize, result);
                current.RemoveAt(current.Count - 1);
            }
        }

        public static List<int> GetRelatedBookIds(int bookId, List<List<int>> frequentItemsets)
        {
            return frequentItemsets
                .Where(itemset => itemset.Contains(bookId))
                .SelectMany(itemset => itemset)
                .Where(id => id != bookId)
                .Distinct()
                .ToList();
        }
    }
}