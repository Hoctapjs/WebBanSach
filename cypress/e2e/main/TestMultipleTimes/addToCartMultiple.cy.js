describe("Thêm sản phẩm vào giỏ hàng", () => {
  let testData;

  before(() => {
    cy.fixture("cartProductMultiple").then((data) => {
      testData = data;
    });
  });

  it("Người dùng có thể thêm từng sản phẩm vào giỏ và kiểm tra thông tin hiển thị", () => {
    testData.forEach((product, index) => {
      // Truy cập lại trang chủ trước mỗi sản phẩm
      cy.visit("https://booksaw.huynhthanhson.io.vn/");

      cy.get(".product-item").eq(index).click();
      cy.get(".btn-add-to-cart.add-to-cart-btn").click();
    });

    cy.get(".cart-link").click();
    cy.get(".cart-table tbody tr").should(
      "have.length.at.least",
      testData.length
    );

    testData.forEach((product, index) => {
      cy.get(".cart-table tbody tr")
        .eq(index)
        .should("contain.text", product.productName);
      cy.get(".cart-table tbody tr")
        .eq(index)
        .find("td")
        .eq(3)
        .should("contain.text", product.expectedQuantity);
    });
  });
});
