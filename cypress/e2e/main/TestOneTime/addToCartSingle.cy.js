describe("Thêm sản phẩm vào giỏ hàng", () => {
  let testData;

  before(() => {
    cy.fixture("cartProductSingle").then((data) => {
      testData = data;
    });
  });

  it("Người dùng có thể thêm sản phẩm vào giỏ và kiểm tra trong giỏ", () => {
    cy.visit("https://booksaw.huynhthanhson.io.vn/");

    // Chọn sản phẩm đầu tiên
    cy.get(".product-item").first().click();

    // Nhấn nút thêm vào giỏ hàng
    cy.get(".btn-add-to-cart.add-to-cart-btn").click();

    // Nhấn vào biểu tượng giỏ hàng để mở giỏ
    cy.get(".cart-link").click();

    // Kiểm tra giỏ có chứa ít nhất 1 sản phẩm
    cy.get(".cart-table tbody tr").should("have.length.at.least", 1);

    // Kiểm tra tên sản phẩm hiển thị đúng
    cy.get(".cart-table tbody tr")
      .first()
      .should("contain.text", testData.productName);

    // Kiểm tra số lượng sản phẩm đúng
    cy.get(".cart-table tbody tr")
      .first()
      .find("td")
      .eq(3)
      .should("contain.text", testData.expectedQuantity);
  });
});
