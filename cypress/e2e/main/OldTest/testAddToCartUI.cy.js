describe("Thêm sản phẩm vào giỏ hàng và đặt hàng thành công", () => {
  it("Hiển thị trang Thank You sau khi mua hàng", () => {
    // Truy cập trang chủ
    cy.visit("https://booksaw.huynhthanhson.io.vn/");

    // Chọn sản phẩm đầu tiên
    cy.get(".product-item").first().click();

    // Nhấn nút "Mua ngay" (nếu chưa đăng nhập sẽ chuyển về login)
    cy.get(".btn-buy-now").click();

    // Nhấn thanh toán
    cy.get(".btn-checkout").click();

    // Kiểm tra đã chuyển hướng đến trang đăng nhập
    cy.url().should("include", "/Account/Login");

    // Đăng nhập
    cy.get("#Username").type("user");
    cy.get("#Password").type("123456789");
    cy.get(".btn-login").click();

    // Kiểm tra đăng nhập xong thì chuyển về Cart hoặc Checkout
    cy.url().should("include", "/Cart");

    // Click thanh toán lại nếu cần
    cy.get(".btn-checkout").click();

    // Xác nhận đặt hàng
    cy.get(".btn-confirm").click();

    // Kiểm tra trang cảm ơn xuất hiện
    cy.url().should("include", "/Order/ThankYou");
    // cy.contains('Thank You For Your Order!').should('be.visible');
    // cy.get('h2').should('contain.text', 'Thank You For Your Order!');

    // Kiểm tra ko quan tâm style
    cy.get("h2").should("have.text", "Thank you for your order!");

    // Kiểm tra text chính xác
    // cy.get('h2').should('contain.text', 'Thank You For Your Order!');
  });
});
