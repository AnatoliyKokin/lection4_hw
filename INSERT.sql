INSERT INTO persons VALUES (3333444444, 'Иван', 'Иванов');
INSERT INTO persons VALUES (5556666666, 'Петр', 'Петров');
INSERT INTO persons VALUES (7777777556, 'Сидор', 'Сидоров');
INSERT INTO persons VALUES (4566734556, 'Сергей', 'Сергеев');
INSERT INTO persons VALUES (1234566789, 'Николай', 'Николаев');

INSERT INTO currencies VALUES ('RU', 'Рубль', 'Россия');
INSERT INTO currencies VALUES ('CNY', 'Юань', 'Китай');
INSERT INTO currencies VALUES ('IRR', 'Риал', 'Иран');
INSERT INTO currencies VALUES ('KZT', 'Тенге', 'Казахстан');
INSERT INTO currencies VALUES ('TRY', 'Лира', 'Турция');

INSERT INTO deposits(person,currency,balance) VALUES (3333444444, 'RU', 1000);
INSERT INTO deposits(person,currency,balance) VALUES (5556666666, 'CNY', 1000000);
INSERT INTO deposits(person,currency,balance) VALUES (7777777556, 'IRR', 500000);
INSERT INTO deposits(person,currency,balance) VALUES (4566734556, 'KZT', 10000000);
INSERT INTO deposits(person,currency,balance) VALUES (1234566789, 'TRY', 100.1);
INSERT INTO deposits(person,currency,balance) VALUES (3333444444, 'CNY', 1001.11);
INSERT INTO deposits(person,currency,balance) VALUES (3333444444, 'IRR', 1002.212);
