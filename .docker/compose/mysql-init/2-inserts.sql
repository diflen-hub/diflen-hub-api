SET NAMES utf8mb4;
SET CHARACTER SET utf8mb4;

use diflenhub;

insert into users(
    public_id,
    email,
    username,
    password
) values (
    "33e5cabd-e14a-415a-b94d-421bead93a35",
    "7lucasdaniel@gmail.com",
    "prolud",
    "$2a$11$uPn7itAIXQqxBhMeTq.1QeAD8RO70fVL9SGrGXR61v1KYJEd/VJ/G"
);

insert into unities(public_id, name, description)
values
    ("094148cd-1f48-4433-adac-fde4b85bc4f3", "Teologias Perigosas", null),
    ("613c10a9-7f33-453b-a704-56a85679727b", "O Poder do Amor", "Descubra “O Poder do Amor” através de uma série inspiradora. Explore como fluir no melhor de Deus, fortalecer sua fé e transformar seus relacionamentos. Testemunhe histórias de superação, como uma jornada de perdão trouxe libertação e cura."),
    ("d94656ae-9350-4cbd-aefe-a668e22bb35e", "Jejum e Oração", null);