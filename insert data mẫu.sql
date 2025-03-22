INSERT INTO Categories (Name) VALUES 
('Fiction'),
('Science'),
('Business'),
('Technology'),
('Romance');

INSERT INTO Categories (Name) VALUES 
('Manga')

INSERT INTO Books (Title, Author, Price, Description, ImageUrl, Stock, CategoryId) VALUES 
('The Great Adventure', 'John Smith', 29.99, 'A thrilling adventure story', '~/Asset/images/product-item7.jpg', 50, 1),
('Artificial Intelligence Basics', 'Jane Doe', 45.00, 'Introduction to AI concepts', '~/Asset/images/product-item7.jpg', 30, 4),
('Business Growth Hacks', 'Elon Stark', 38.99, 'Tips to scale your business', '~/Asset/images/product-item7.jpg', 40, 3),
('The Science of Everything', 'Neil Tyson', 50.00, 'A deep dive into scientific discoveries', '~/Asset/images/product-item7.jpg', 25, 2),
('Love in Paris', 'Emma Watson', 27.50, 'A romantic novel set in Paris', '~/Asset/images/product-item7.jpg', 35, 5);

go
INSERT INTO Books (Title, Author, Price, Description, ImageUrl, Stock, CategoryId) VALUES 
('Mastering Python', 'Guido Van Rossum', 55.99, 'Comprehensive guide to Python programming', '~/Asset/images/product-item7.jpg', 20, 4),
('Startup Essentials', 'Mark Cuban', 42.00, 'Guide to launching a successful startup', '~/Asset/images/product-item7.jpg', 15, 3),
('The Universe Explained', 'Stephen Hawking', 60.00, 'Understanding the cosmos', '~/Asset/images/product-item7.jpg', 18, 2),
('Mystery at Midnight', 'Agatha Christie', 32.50, 'A classic detective novel', '~/Asset/images/product-item7.jpg', 22, 5),
('Cooking with Love', 'Gordon Ramsay', 28.75, 'Delicious recipes for home chefs', '~/Asset/images/product-item7.jpg', 40, 6),
('One Piece Vol. 1', 'Eiichiro Oda', 12.99, 'The beginning of Luffy’s pirate adventure', '~/Asset/images/product-item7.jpg', 100, 6),
('Naruto Vol. 1', 'Masashi Kishimoto', 11.50, 'The story of Naruto Uzumaki, a young ninja', '~/Asset/images/product-item7.jpg', 90, 6),
('Attack on Titan Vol. 1', 'Hajime Isayama', 13.75, 'Humanity’s battle against the Titans', '~/Asset/images/product-item7.jpg', 85, 6),
('Demon Slayer Vol. 1', 'Koyoharu Gotouge', 14.99, 'Tanjiro’s journey to avenge his family', '~/Asset/images/product-item7.jpg', 80, 6),
('Dragon Ball Vol. 1', 'Akira Toriyama', 10.99, 'The adventures of Goku in search of Dragon Balls', '~/Asset/images/product-item7.jpg', 95, 6);

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





