const viewports = [
  { device: 'Desktop', width: 1280, height: 800 },
  { device: 'Tablet', width: 768, height: 1024 },
  { device: 'Mobile', width: 375, height: 667 },
];

viewports.forEach(({ device, width, height }) => {
  describe(`Thêm sản phẩm vào giỏ hàng và đặt hàng thành công trên ${device}`, () => {
    it('Hiển thị trang Thank You sau khi mua hàng', () => {
      // Thiết lập viewport theo thiết bị
      cy.viewport(width, height);

      // Truy cập trang chủ
      cy.visit('https://booksaw.huynhthanhson.io.vn/');

      // Chọn sản phẩm đầu tiên
      cy.get('.product-item').first().click();

      // Nhấn nút "Mua ngay"
      cy.get('.btn-buy-now').click({ force: true });

      // Nhấn thanh toán
      cy.get('.btn-checkout').click();

      // Kiểm tra đã chuyển hướng đến trang đăng nhập
      cy.url().should('include', '/Account/Login');

      // Đăng nhập
      cy.get('#Username').type('user');
      cy.get('#Password').type('123456789');
      cy.get('.btn-login').click();

      // Kiểm tra chuyển về Cart
      cy.url().should('include', '/Cart');

      // Tiến hành thanh toán
      cy.get('.btn-checkout').click();

      // Xác nhận đặt hàng
      cy.get('.btn-confirm').click();

      // Kiểm tra trang cảm ơn
      cy.url().should('include', '/Order/ThankYou');

      // Kiểm tra nội dung trang cảm ơn
      cy.get('h2').should('have.text', 'Thank you for your order!');
    });
  });
});
