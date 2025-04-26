describe('Thêm sản phẩm vào giỏ hàng', () => {
  it('Người dùng chưa đăng nhập sẽ được chuyển đến trang đăng nhập', () => {
    cy.visit('https://booksaw.huynhthanhson.io.vn/'); // Truy cập trang web

    cy.get('.product-item').first().click(); // Chọn sản phẩm đầu tiên
    // cy.get('.btn-accent-arrow').click(); // Nhấn nút thêm vào giỏ hàng
    cy.get('.btn-buy-now').click();

    cy.get('.btn-checkout').click();

    // Kiểm tra nếu bị chuyển hướng sang trang đăng nhập
    cy.url().should('include', '/Account/Login?ReturnUrl=%2FOrder%2FCheckout');
    // cy.url().should('include', '/Cart');

    // Nhập thông tin đăng nhập
    cy.get('#Username').type('user');
    cy.get('#Password').type('123456789');
    cy.get('.btn-login').click(); // Nhấn nút đăng nhập

    // kiểm tra sau khi đăng nhập / thông tin mua hàng đúng chưa / sản phẩm tồn kho / kiểm tra hello user đúng chưa

    // kiểm tra đn thành công thì về trang check out
    cy.url().should('include', '/Cart');
    cy.get('.btn-checkout').click();
    cy.get('.btn-confirm').click();

    // kiểm tra đặt hàng thành công -> hiển thị trang cảm ơn
    cy.url().should('include', '/Order/ThankYou');

    // Kiểm tra đăng nhập thành công (có thể redirect về trang chủ)
    // cy.url().should('not.include', '/login');

    // Quay lại chọn sản phẩm và thêm vào giỏ hàng
    // cy.get('.product-item').first().click();
    // cy.get('.cart-link').click();

    // Kiểm tra giỏ hàng có 1 sản phẩm
    // cy.get('.cart-table tbody tr').should('have.length', 1);
  });
  // it('Người dùng chưa đăng nhập sẽ được chuyển đến trang đăng nhập', () => {
  //   cy.visit('https://booksaw.huynhthanhson.io.vn/'); // Truy cập trang web

  //   cy.get('.product-item').first().click(); // Chọn sản phẩm đầu tiên
  //   cy.get('.btn-checkout').click(); // Nhấn nút thêm vào giỏ hàng

  //   // Kiểm tra nếu bị chuyển hướng sang trang đăng nhập
  //   cy.url().should('include', '/Account/Login?ReturnUrl=%2FCart');

  //   // Nhập thông tin đăng nhập
  //   cy.get('#Username').type('thanhson2');
  //   cy.get('#Password').type('123456789222');
  //   cy.get('.btn-login').click(); // Nhấn nút đăng nhập

  //   // Kiểm tra đăng nhập thành công (có thể redirect về trang chủ)
  //   cy.url().should('not.include', '/login');

  //   // Quay lại chọn sản phẩm và thêm vào giỏ hàng
  //   // cy.get('.product-item').first().click();
  //   cy.get('.cart-link').click();

  //   // Kiểm tra giỏ hàng có 1 sản phẩm
  //   cy.get('.cart-table tbody tr').should('have.length', 1);
  // });
});
