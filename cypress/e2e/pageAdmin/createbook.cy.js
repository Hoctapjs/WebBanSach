describe("Test chức năng Thêm sách mới tại trang Admin", () => {
  beforeEach(() => {
    Cypress.on("uncaught:exception", (err, runnable) => {
      return false;
    });

    cy.visit("https://booksaw.huynhthanhson.io.vn/Admin");
    cy.get('input[name="Username"]').type("thanhson");
    cy.get('input[name="Password"]').type("123456789");
    cy.get(".btn-login").click();

    cy.url().should("include", "/Admin");
    cy.get('a[href*="/Admin/Books"]').click();
    cy.get('a[href*="/Admin/Create_book"]').click();
    cy.url().should("include", "/Admin/Create_book");
  });

  it("TC1 - Thêm sách thành công với đầy đủ dữ liệu hợp lệ", () => {
    cy.get("#Title").type("Cẩm Nang Kiểm Thử Tự Động");
    cy.get("#Description").type("Cuốn sách chuyên sâu về kiểm thử phần mềm.");
    cy.get("#Author").type("Lê Văn Kiểm");
    cy.get("#Price").type("145000");
    cy.get("#imageFile").attachFile("test.jpg"); // đặt file test.jpg trong cypress/fixtures
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.url().should("include", "/Admin/Books");
  });

  it("TC2 - Thiếu tiêu đề", () => {
    cy.get("#Description").type("Mô tả sách...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Tiêu đề là bắt buộc").should("exist");
  });

  it("TC3 - Thiếu mô tả", () => {
    cy.get("#Title").type("Sách thiếu mô tả");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Mô tả là bắt buộc").should("exist");
  });

  it("TC4 - Thiếu tác giả", () => {
    cy.get("#Title").type("Sách không có tác giả");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Tác giả là bắt buộc").should("exist");
  });

  it("TC5 - Giá âm", () => {
    cy.get("#Title").type("Sách giá âm");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("-50000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Giá không hợp lệ").should("exist");
  });

  it("TC6 - Không chọn ảnh", () => {
    cy.get("#Title").type("Sách không có ảnh");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Vui lòng chọn ảnh").should("exist");
  });

  it("TC7 - Không chọn thể loại", () => {
    cy.get("#Title").type("Sách chưa chọn thể loại");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Chọn thể loại");
    cy.get('button[type="submit"]').click();

    cy.contains("Vui lòng chọn thể loại").should("exist");
  });

  it("TC8 - Upload file sai định dạng", () => {
    cy.get("#Title").should("not.be.disabled").type("File sai định dạng");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.txt"); // Chọn test.txt chứ không phải file ảnh, file đặt trong fixtures
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Định dạng ảnh không hợp lệ").should("exist");
  });

  it("TC9 - Không chọn giá", () => {
    cy.get("#Title").type("Sách giá âm");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("The Price field is required.").should("exist");
  });

  it("TC10 - Giá thập phân", () => {
    cy.get("#Title").type("Sách giá âm");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("0.01");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Giá không hợp lệ").should("exist");
  });

  it("TC11 - Giá bằng 0", () => {
    cy.get("#Title").type("Giá bằng 0");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("0");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Giá không hợp lệ").should("exist");
  });

  it("TC12 - Giá quá lớn", () => {
    cy.get("#Title").type("Giá quá lớn");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("1000000000"); // Giá sách là 1 tỷ
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Giá không hợp lệ").should("exist");
  });

  it("TC13 - Tiêu đề quá dài", () => {
    cy.get("#Title").type("A".repeat(300)); // Giả sử giới hạn là 255 ký tự cho tiêu đề thôi
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Tiêu đề quá dài").should("exist");
  });

  it("TC14 - Mô tả quá dài", () => {
    cy.get("#Title").type("Mô tả dài");
    cy.get("#Description").type("B".repeat(2000)); // giả định giới hạn là 1000 ký tự
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Mô tả quá dài").should("exist");
  });

  it("TC15 - Ảnh dung lượng lớn", () => {
    cy.get("#Title").type("Sách ảnh lớn");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("large-image.jpg"); // ảnh > 3MB
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Ảnh quá lớn").should("exist");
  });

  it("TC16 - Tác giả có khoảng trắng đầu/cuối", () => {
    cy.get("#Title").type("Tác giả trắng");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("   Nguyễn Văn A   ");
    cy.get("#Price").type("100000");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Tác giả không hợp lệ").should("exist");
  });

  it("TC17 - Giá là chữ", () => {
    cy.get("#Title").type("Giá là chữ");
    cy.get("#Description").type("Mô tả...");
    cy.get("#Author").type("Tác giả");
    cy.get("#Price").type("abcde");
    cy.get("#imageFile").attachFile("test.jpg");
    cy.get("#CategoryId").select("Technology");
    cy.get('button[type="submit"]').click();

    cy.contains("Giá không hợp lệ").should("exist");
  });

  it("TC18 - Gửi form trống", () => {
    cy.get('button[type="submit"]').click();

    cy.contains("Tiêu đề là bắt buộc").should("exist");
    cy.contains("Mô tả là bắt buộc").should("exist");
    cy.contains("Tác giả là bắt buộc").should("exist");
    cy.contains("The Price field is required.").should("exist");
    cy.contains("Vui lòng chọn ảnh").should("exist");
    cy.contains("Vui lòng chọn thể loại").should("exist");
  });
});
