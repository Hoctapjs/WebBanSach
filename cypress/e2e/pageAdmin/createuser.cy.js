describe("Test chức năng Thêm user mới tại trang Admin", () => {
  beforeEach(() => {
    Cypress.on("uncaught:exception", (err, runnable) => {
      return false;
    });

    cy.visit("https://booksaw.huynhthanhson.io.vn/Admin");
    cy.get('input[name="Username"]').type("thanhson");
    cy.get('input[name="Password"]').type("123456789");
    cy.get(".btn-login").click();

    cy.url().should("include", "/Admin");
    cy.get('a[href*="/Admin/Users"]').click();
    cy.get('a[href*="/Admin/Create_user"]').click();
    cy.url().should("include", "/Admin/Create_user");
  });

  it("TC1 - Tạo user thành công với đầy đủ dữ liệu hợp lệ", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("Admin");
    cy.get('button[type="submit"]').click();

    cy.url().should("include", "/Admin/Users");
  });

  it("TC2 - Thiếu tên đăng nhập", () => {
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("Admin");
    cy.get('button[type="submit"]').click();

    cy.contains("Tên đăng nhập là bắt buộc").should("exist");
  });

  it("TC3 - Thiếu họ tên đầy đủ", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("Admin");
    cy.get('button[type="submit"]').click();

    cy.contains("Họ tên là bắt buộc").should("exist");
  });

  it("TC4 - Thiếu email", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("Admin");
    cy.get('button[type="submit"]').click();

    cy.contains("Email là bắt buộc").should("exist");
  });

  it("TC5 - Email không hợp lệ", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#Email").type("notanemail");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("Admin");
    cy.get('button[type="submit"]').click();

    cy.contains("Email không hợp lệ").should("exist");
  });

  it("TC6 - Thiếu mật khẩu", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#Role").select("Admin");
    cy.get('button[type="submit"]').click();

    cy.contains("Mật khẩu là bắt buộc").should("exist");
  });

  it("TC7 - Không chọn vai trò", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("Chọn vai trò");
    cy.get('button[type="submit"]').click();

    cy.contains("Vui lòng chọn vai trò").should("exist");
  });

  it("TC8 - Username quá dài", () => {
    cy.get("#UserName").type("a".repeat(100)); // giả sử max 50 ký tự
    cy.get("#FullName").type("Nguyễn Văn A");
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("User");
    cy.get('button[type="submit"]').click();

    cy.contains("Tên đăng nhập quá dài").should("exist");
  });

  it("TC9 - Họ tên quá dài", () => {
    cy.get("#UserName").type("nguyenvana");
    cy.get("#FullName").type("b".repeat(500)); // giả sử max 255
    cy.get("#Email").type("vana@gmail.com");
    cy.get("#PasswordHash").type("123456");
    cy.get("#Role").select("User");
    cy.get('button[type="submit"]').click();

    cy.contains("Họ tên quá dài").should("exist");
  });

  it("TC10 - Gửi form trống", () => {
    cy.get('button[type="submit"]').click();

    cy.contains("Tên đăng nhập là bắt buộc").should("exist");
    cy.contains("Họ tên là bắt buộc").should("exist");
    cy.contains("Email là bắt buộc").should("exist");
    cy.contains("Mật khẩu là bắt buộc").should("exist");
    cy.contains("Vui lòng chọn vai trò").should("exist");
  });
});
