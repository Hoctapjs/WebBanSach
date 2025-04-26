describe("Thêm sản phẩm vào giỏ hàng", () => {
  it("Người dùng có thể thêm sản phẩm vào giỏ và kiểm tra trong giỏ", () => {
    cy.visit("https://booksaw.huynhthanhson.io.vn/");

    // Chọn sản phẩm đầu tiên
    cy.get(".product-item").first().click();

    // Nhấn nút thêm vào giỏ hàng
    cy.get(".btn-add-to-cart.add-to-cart-btn").click(); // hoặc class tương ứng nếu có

    // Nhấn vào biểu tượng giỏ hàng để mở giỏ
    cy.get(".cart-link").click();

    // Kiểm tra giỏ có chứa sản phẩm đó
    cy.get(".cart-table tbody tr").should("have.length.at.least", 1);

    // Kiểm tra tên sản phẩm hiển thị đúng
    cy.get(".cart-table tbody tr")
      .first()
      .should("contain.text", "Deep Fire Rising");

    // Có thể kiểm tra số lượng nếu muốn
    cy.get(".cart-table tbody tr")
      .first()
      .find("td")
      .eq(3) // cột Quantity
      .should("contain.text", "1"); // hoặc 2,3 nếu test lặp nhiều lần
  });
});
