describe("Mua ngay và hoàn tất đơn hàng", () => {
  let productData;
  let userData;

  before(() => {
    cy.fixture("cartProductMultiple").then((data) => {
      productData = data;
    });
    cy.fixture("userAccountMultiple").then((data) => {
      userData = data[0]; // chọn tài khoản đầu tiên
    });
  });

  it("Người dùng có thể mua nhiều sản phẩm và được chuyển đến trang cảm ơn", () => {
    productData.forEach((product, index) => {
      //   cy.visit("https://booksaw.huynhthanhson.io.vn/");
      cy.visit("http://localhost:62416/");
      cy.get(".product-item").eq(index).click();
      cy.get(".btn-add-to-cart.add-to-cart-btn").click();
    });

    cy.get("#cart-quantity").should("contain.text", "2");

    cy.get(".cart-link").click();
    cy.get(".btn-checkout").click();

    cy.url().should("include", "/Account/Login");
    // cy.get("#Username").type(userData.username);
    // cy.get("#Password").type(userData.password);
    // cy.get(".btn-login").click();
    cy.login(userData.username, userData.password);

    cy.url().should("include", "/Cart");
    cy.get(".btn-checkout").click();

    productData.forEach((product, index) => {
      cy.get(".cart-table tbody tr")
        .eq(index)
        .should("contain.text", product.productName);
      cy.get(".cart-table tbody tr")
        .eq(index)
        .find("td")
        .eq(3)
        .should("contain.text", product.expectedQuantity);
    });

    cy.get(".btn-confirm").click();

    cy.get("#cart-quantity").should("contain.text", "0");
    cy.url().should("include", "/Order/ThankYou");
    cy.get("h2").should("have.text", "Thank you for your order!");
  });
});
