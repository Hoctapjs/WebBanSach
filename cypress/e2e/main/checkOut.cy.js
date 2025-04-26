describe("Mua ngay và hoàn tất đơn hàng", () => {
  it("Người dùng chưa đăng nhập vẫn có thể mua và được chuyển tới trang cảm ơn", () => {
    cy.visit("https://booksaw.huynhthanhson.io.vn/");

    // Chọn sản phẩm đầu tiên
    cy.get(".product-item").first().click();

    // Nhấn nút "Mua ngay"
    cy.get(".btn-buy-now").click();

    // Nhấn thanh toán
    cy.get(".btn-checkout").click();

    // Kiểm tra chuyển sang trang đăng nhập
    cy.url().should("include", "/Account/Login");

    // Đăng nhập
    cy.get("#Username").type("thanhson");
    cy.get("#Password").type("123456789");
    cy.get(".btn-login").click();

    // Sau đăng nhập, kiểm tra quay lại giỏ hàng
    cy.url().should("include", "/Cart");

    // Nhấn nút thanh toán lần nữa
    cy.get(".btn-checkout").click();

    // Nhấn xác nhận đặt hàng
    cy.get(".btn-confirm").click();

    // Kiểm tra trang cảm ơn xuất hiện
    cy.url().should("include", "/Order/ThankYou");

    // Kiểm tra nội dung trang
    cy.get("h2").should("have.text", "Thank you for your order!");
  });
});
