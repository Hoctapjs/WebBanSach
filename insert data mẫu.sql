INSERT INTO Categories (Name) VALUES 
('Fiction'),
('Science'),
('Business'),
('Technology'),
('Romance');

INSERT INTO Books (Title, Author, Price, Description, ImageUrl, Stock, CategoryId) VALUES 
('The Great Adventure', 'John Smith', 29.99, 'A thrilling adventure story', '~/Asset/images/product-item7.jpg', 50, 1),
('Artificial Intelligence Basics', 'Jane Doe', 45.00, 'Introduction to AI concepts', '~/Asset/images/product-item7.jpg', 30, 4),
('Business Growth Hacks', 'Elon Stark', 38.99, 'Tips to scale your business', '~/Asset/images/product-item7.jpg', 40, 3),
('The Science of Everything', 'Neil Tyson', 50.00, 'A deep dive into scientific discoveries', '~/Asset/images/product-item7.jpg', 25, 2),
('Love in Paris', 'Emma Watson', 27.50, 'A romantic novel set in Paris', '~/Asset/images/product-item7.jpg', 35, 5);


INSERT INTO Users (Username, PasswordHash, Email, Role) VALUES 
('admin', 'adminhashedpassword', 'admin@example.com', 'Admin'),
('john_doe', 'hashedpassword123', 'john@example.com', 'Customer'),
('alice_wonder', 'securepass456', 'alice@example.com', 'Customer');

INSERT INTO Orders (UserId, OrderDate, Status) VALUES 
(2, '2025-03-20', 'Completed'),
(3, '2025-03-21', 'Pending');

INSERT INTO OrderDetails (OrderId, BookId, Quantity, UnitPrice) VALUES 
(1, 1, 2, 29.99),
(1, 3, 1, 38.99),
(2, 2, 1, 45.00),
(2, 5, 3, 27.50);

INSERT INTO ShoppingCartItems (CartId, BookId, Quantity) VALUES 
('session_1', 1, 1),
('session_1', 3, 2),
('session_2', 5, 1);





