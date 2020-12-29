CREATE TABLE CIDADE( id integer PRIMARY KEY NOT NULL UNIQUE,
                     nome TEXT NOT NULL,
                     id_estado integer NOT NULL,
                     CONSTRAINT fk_estado FOREIGN KEY ( id_estado ) REFERENCES ESTADO(id) );