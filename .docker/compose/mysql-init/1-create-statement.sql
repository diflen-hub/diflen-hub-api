use diflenhub;

create table users(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    username varchar(30) not null unique,
    password varchar(255) not null,
    experience int default 0,
    status boolean default true,
    file_type varchar(30),
    profile_picture blob
);

create table unities(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    name varchar(255) not null unique,
    description text
);

create table certificates(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    unity_id int not null,
    user_id int not null,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP not null,
    foreign key (unity_id) references unities(id),
    foreign key (user_id) references users(id)
);

create table lessons(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    title varchar(100) not null unique,
    description text,
    sequence int not null,
    video_url varchar(255) not null,
    unity_id int not null,
    foreign key (unity_id) references unities(id)
);

create table questions(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    statement text not null,
    lesson_id int not null,
    unity_id int not null,
    foreign key (lesson_id) references lessons(id),
    foreign key (unity_id) references unities(id)
);

create table alternatives(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    text text not null,
    is_correct boolean not null,
    question_id int not null,
    foreign key (question_id) references questions(id)
);

create table answers(
    id int primary key auto_increment,
    public_id varchar(36) not null unique,
    alternative_id int not null,
    user_id int not null,
    question_id int not null,
    lesson_id int not null,
    unity_id int not null,
    is_correct boolean not null,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP not null,
    foreign key (alternative_id) references alternatives(id),
    foreign key (user_id) references users(id),
    foreign key (question_id) references questions(id),
    foreign key (lesson_id) references lessons(id),
    foreign key (unity_id) references unities(id)
)