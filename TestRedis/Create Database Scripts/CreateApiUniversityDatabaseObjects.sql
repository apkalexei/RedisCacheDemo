CREATE TABLE [dbo].[Room](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[RoomNumber] [int] NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[RoomReservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Room] [int] NOT NULL,
	[TimeSlot] [int] NOT NULL,
	[Reserved] [bit] NOT NULL,
 CONSTRAINT [PK_RoomReservation] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE RoomReservation ADD CONSTRAINT fk_Room FOREIGN KEY (room) REFERENCES Room(id)

-- rooms
insert into room (name,roomnumber) values ('Gym',1)
insert into room (name,roomnumber) values ('Math Room',100)
insert into room (name,roomnumber) values ('Chemistry Lab',101)
insert into room (name,roomnumber) values ('Lecture 1',102)
insert into room (name,roomnumber) values ('Lecture 2',103)
insert into room (name,roomnumber) values ('Art Room',104)

-- reservations
insert into roomreservation (room,timeslot,reserved) values (1,1,1)
insert into roomreservation (room,timeslot,reserved) values (1,2,0)
insert into roomreservation (room,timeslot,reserved) values (1,3,0)
insert into roomreservation (room,timeslot,reserved) values (2,1,0)
insert into roomreservation (room,timeslot,reserved) values (2,2,1)
insert into roomreservation (room,timeslot,reserved) values (2,3,1)
insert into roomreservation (room,timeslot,reserved) values (3,1,1)
insert into roomreservation (room,timeslot,reserved) values (3,2,0)
insert into roomreservation (room,timeslot,reserved) values (3,3,0)
insert into roomreservation (room,timeslot,reserved) values (4,1,1)
insert into roomreservation (room,timeslot,reserved) values (4,2,1)
insert into roomreservation (room,timeslot,reserved) values (4,3,0)
insert into roomreservation (room,timeslot,reserved) values (5,1,0)
insert into roomreservation (room,timeslot,reserved) values (5,2,1)
insert into roomreservation (room,timeslot,reserved) values (5,3,0)
insert into roomreservation (room,timeslot,reserved) values (6,1,1)
insert into roomreservation (room,timeslot,reserved) values (6,2,1)
insert into roomreservation (room,timeslot,reserved) values (6,3,1)

