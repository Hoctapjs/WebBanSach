describe("Test chức năng Thêm đơn hàng mới tại trang Admin", () => {
  beforeEach(() => {
    Cypress.on("uncaught:exception", (err, runnable) => {
      return false;
    });

    cy.visit("https://booksaw.huynhthanhson.io.vn/Admin");
    cy.get('input[name="Username"]').type("thanhson");
    cy.get('input[name="Password"]').type("123456789");
    cy.get(".btn-login").click();

    cy.url().should("include", "/Admin");
    cy.get('a[href*="/Admin/Orders"]').click();
    cy.get('a[href*="/Admin/Create_order"]').click();
    cy.url().should("include", "/Admin/Create_order");
  });

  it("TC1 - Tạo đơn hàng thành công với đầy đủ dữ liệu hợp lệ", () => {
    cy.get("#UserId").type("2");
    cy.get("#OrderDate").type("2025-04-26");
    cy.get("#Status").type("Completed");
    cy.get('button[type="submit"]').click();

    cy.url().should("include", "/Admin/Orders");
    // cy.contains("Tạo đơn hàng thành công").should("exist");
  });

  it("TC2 - Thiếu UserId", () => {
    cy.get("#OrderDate").type("2025-04-26");
    cy.get("#Status").type("Completed");
    cy.get('button[type="submit"]').click();

    cy.contains("UserId là bắt buộc").should("exist");
  });

  it("TC3 - Thiếu ngày đặt hàng", () => {
    cy.get("#UserId").type("2");
    cy.get("#Status").type("Completed");
    cy.get('button[type="submit"]').click();

    cy.contains("Ngày đặt hàng là bắt buộc").should("exist");
  });

  it("TC4 - Thiếu trạng thái đơn hàng", () => {
    cy.get("#UserId").type("2");
    cy.get("#OrderDate").type("2025-04-26");
    cy.get('button[type="submit"]').click();

    cy.contains("Trạng thái đơn hàng là bắt buộc").should("exist");
  });

  it("TC5 - Nhập UserId không hợp lệ (text thay vì số)", () => {
    cy.get("#UserId").type("abc");
    cy.get("#OrderDate").type("2025-04-26");
    cy.get("#Status").type("Completed");
    cy.get('button[type="submit"]').click();

    cy.contains("UserId không hợp lệ").should("exist");
  });

  it("TC86 - Ngày đặt hàng là ngày quá khứ", () => {
    cy.get("#UserId").type("2");
    cy.get("#OrderDate").type("2020-01-01"); // Ngày quá khứ
    cy.get("#Status").type("Completed");
    cy.get('button[type="submit"]').click();

    cy.contains("Ngày đặt hàng không được nhỏ hơn hiện tại").should("exist");
  });

  it("TC7 - Trạng thái đơn hàng không hợp lệ", () => {
    cy.get("#UserId").type("2");
    cy.get("#OrderDate").type("2025-04-26");
    // Không chọn trạng thái nào hoặc chọn giá trị không hợp lệ
    cy.get("#Status").type("Không có trạng thái751328");
    cy.get('button[type="submit"]').click();

    cy.contains("Vui lòng chọn trạng thái đơn hàng hợp lệ").should("exist");
  });

  it("TC8 - Gửi form trống", () => {
    cy.get('button[type="submit"]').click();

    cy.contains("UserId là bắt buộc").should("exist");
    cy.contains("Ngày đặt hàng là bắt buộc").should("exist");
    cy.contains("Trạng thái đơn hàng là bắt buộc").should("exist");
  });

  it("TC9 - UserId không tồn tại", () => {
    cy.get("#UserId").type("99"); // Ví dụ user ID 99 không tồn tại
    cy.get("#OrderDate").type("2025-04-26");
    cy.get("#Status").type("Pending");
    cy.get('button[type="submit"]').click();

    cy.contains("User không tồn tại").should("exist");
  });
});
