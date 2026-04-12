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

-- insert into certificates(public_id, unity_id, user_id)
-- values
--     ("522e8efd-5829-4756-a27b-b502cf779c15", 1, 1);

insert into lessons(public_id, title, description, sequence, video_url, unity_id)
values
    ("7c5d85c4-6f70-4dea-9765-3f2a845dee4d", "Como Identificar um Ensino Falso", 'Nesta primeira mensagem da série "Teologias Perigosas"[...] na fé cristã.', 1, "https://youtu.be/7r1qARCbL8I?si=68QLE6yqbYFTm7jg", 1),
    ("5cbf7864-e6cc-4754-bdca-230400ba4b96", "A Bíblia Tem Algum Erro?", 'Na segunda mensagem da série “Teologias Perigosas”[...] fortalecer sua fé.', 2, "https://youtu.be/zvsLciGgqVU?si=1Z7Z0xYguQJVncYh", 1),
    ("92910054-4a3a-4920-a09b-4be53221d4cf", "Cuidado com Esses Ensinos", 'Na terceira e última mensagem da série “Teologias Perigosas”[...] da graça bíblica.', 3, "https://youtu.be/4QwlBR7qETw?si=dIpNmzx3qCLlQGUj", 1);

insert into questions(public_id, statement, lesson_id, unity_id)
values
    ("477c3b07-2df9-4c29-aa4c-2a50e1f6cecb", "Título questão 1", 1, 1),
    ("3d1339a8-cf03-4e7e-807d-a0bf58fb0eed", "Título questão 2", 1, 1),
    ("31b57f3a-2023-43df-aec2-22d3dff18d3b", "Título questão 3", 2, 1),
    ("ed9563ae-39bc-4e79-999a-23f89bc7d55b", "Título questão 4", 2, 1),
    ("edae9563-39bc-4e79-999a-23f7d5589bcb", "Título questão 5", 3, 1),
    ("ed9ae563-39bc-4e79-999a-d55b23f89bc7", "Título questão 6", 3, 1);

insert into alternatives(public_id, text, is_correct, question_id)
values
    ("15e4d450-3021-464b-bb2d-1c5310d5e3ef", "Texto f da alternativa", false, 1),
    ("e8d8c48d-1a7e-4923-a1d5-4f3f6dc3caa3", "Texto 3 da alternativa", true, 1),
    ("107afd69-5887-4e0f-95cb-cba3149364eb", "Texto b da alternativa", false, 1),
    ("d0a0ea57-fb42-4252-bff7-e0d976582565", "Texto 5 da alternativa", false, 1),
    ("2820ffcc-763f-4269-ba45-159be9b2ca45", "Texto 5 da alternativa", false, 2),
    ("0f82cf91-5d14-4412-9524-5b49ad37e010", "Texto 0 da alternativa", false, 2),
    ("a166575b-4d2f-470b-9f4c-d61a2a12ee31", "Texto 1 da alternativa", true, 2),
    ("08aed2c2-a942-4b6f-a51b-f210f9aa5b4b", "Texto b da alternativa", false, 2),
    ("aa945fc1-1f55-45d8-9bc4-deae9661dd4c", "Texto c da alternativa", true, 3),
    ("7c7f1225-3ca2-4683-b1f7-ce5665b91763", "Texto 3 da alternativa", false, 3),
    ("9868a674-3d22-461a-aad7-8d9baaae0099", "Texto 9 da alternativa", false, 3),
    ("4430c92d-962f-4488-9afe-2a206c078e1a", "Texto a da alternativa", false, 3),
    ("7eaae2d7-025b-4d0f-90d7-04e9d18ff551", "Texto 1 da alternativa", false, 4),
    ("92c03761-a791-446a-88e3-b864c30f86d8", "Texto 8 da alternativa", false, 4),
    ("4425d74d-f745-4703-8e86-24293847723c", "Texto c da alternativa", true, 4),
    ("0b9ff773-6737-4b81-9798-380321448c85", "Texto 5 da alternativa", false, 4),
    ("3ea204bf-e906-42ef-bcb5-4fab4004c1e9", "Texto 9 da alternativa", false, 5),
    ("166a9c3b-efba-4ce4-b35f-2e886948a668", "Texto 8 da alternativa", false, 5),
    ("1647463a-5562-4d5c-bf40-2356c2a223b5", "Texto 5 da alternativa", true, 5),
    ("1f0c6db9-fa88-4dd5-b4c8-36852d7e0827", "Texto 7 da alternativa", false, 5),
    ("96d9db95-403c-4f80-86ec-51108e730c88", "Texto 8 da alternativa", true, 6),
    ("ce4ba466-a7a4-4bf4-b694-26feaca28f90", "Texto 0 da alternativa", false, 6),
    ("91510451-61c5-481c-99be-b1b1a16313ae", "Texto e da alternativa", false, 6),
    ("4fa7ad9f-6b15-463a-b898-f050483d68a9", "Texto 9 da alternativa", false, 6)