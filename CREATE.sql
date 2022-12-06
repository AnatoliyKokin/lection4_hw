CREATE TABLE persons (
	passport bigint PRIMARY KEY,
	first_name varchar(40),
	last_name varchar(40)	
);

CREATE TABLE currencies (
	short_title varchar(10) PRIMARY KEY,
	long_title varchar(20),
	country varchar(40)
);

CREATE TABLE deposits (
	depo_number bigserial PRIMARY KEY,
	person bigint references persons (passport),
	currency varchar(10) references currencies (short_title),
	balance numeric(15,2)
);

