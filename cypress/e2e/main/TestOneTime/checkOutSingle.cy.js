describe("Mua ngay và hoàn tất đơn hàng", () => {
  let productData;
  let userData;

  before(() => {
    cy.fixture("cartProductSingle").then((data) => {
      productData = data;
    });

    cy.fixture("userAccountSingle").then((data) => {
      userData = data;
    });
  });

  it("Người dùng chưa đăng nhập vẫn có thể mua và được chuyển tới trang cảm ơn", () => {
    // cy.visit("https://booksaw.huynhthanhson.io.vn/");
    cy.visit("http://localhost:62416/");

    // Chọn sản phẩm đầu tiên
    cy.get(".product-item").first().click();

    // Nhấn nút "Mua ngay"
    cy.get(".btn-buy-now").click();

    // Nhấn thanh toán
    cy.get(".btn-checkout").click();

    // Kiểm tra chuyển sang trang đăng nhập
    cy.url().should("include", "/Account/Login");

    // Đăng nhập sử dụng dữ liệu từ file JSON
    cy.get("#Username").type(userData.username);
    cy.get("#Password").type(userData.password);
    cy.get(".btn-login").click();

    // Sau đăng nhập, kiểm tra quay lại giỏ hàng
    cy.url().should("include", "/Cart");

    // Nhấn nút thanh toán lại
    cy.get(".btn-checkout").click();

    // Kiểm tra sản phẩm và số lượng
    cy.get(".cart-table tbody tr")
      .first()
      .should("contain.text", productData.productName);

    cy.get(".cart-table tbody tr")
      .first()
      .find("td")
      .eq(3)
      .should("contain.text", productData.expectedQuantity);

    // Xác nhận đơn hàng
    cy.get(".btn-confirm").click();

    // Kiểm tra trang cảm ơn
    cy.url().should("include", "/Order/ThankYou");
    cy.get("h2").should("have.text", "Thank you for your order!");
  });
});
