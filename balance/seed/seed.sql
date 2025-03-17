use balance;
CREATE TABLE IF NOT EXISTS accounts(id varchar(255), balance int, updated_at datetime);

INSERT INTO accounts Values ('f8df753c-3b58-43aa-8016-12aaa4f1ea3e', 200,now());
INSERT INTO accounts Values ('0216ea38-524f-4e85-8743-d484a8f7538e', 200,now());