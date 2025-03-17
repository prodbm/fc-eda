use wallet;
CREATE TABLE IF NOT EXISTS clients(    id varchar(255), name varchar(255), email varchar(255), created_at datetime);


CREATE TABLE IF NOT EXISTS accounts(    id varchar(255), client_id varchar(255), balance int, created_at datetime);

CREATE TABLE IF NOT EXISTS transactions(    id varchar(255), account_id_from varchar(255), account_id_to varchar(255), amount int, created_at datetime);

INSERT INTO clients Values('87495b95-1c7f-4038-ae55-ab36ed6a9411','Ze das Couves','ze@dascouves.com', now());

INSERT INTO clients Values('862f7e09-00d3-11f0-9bd4-0242ac130002','Joao Melao ','joao@melao.com', now());

INSERT INTO accounts Values('f8df753c-3b58-43aa-8016-12aaa4f1ea3e','87495b95-1c7f-4038-ae55-ab36ed6a9411',200, now());

INSERT INTO accounts Values('0216ea38-524f-4e85-8743-d484a8f7538e','862f7e09-00d3-11f0-9bd4-0242ac130002',200, now());