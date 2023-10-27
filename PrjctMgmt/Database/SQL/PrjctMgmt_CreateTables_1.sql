--PrjctMgmt_CreateTables_1.sql
CREATE TABLE [PrjMngSch].[TaskAssign]
(
     [TaskAssignId] [bigint] IDENTITY(1,1) NOT NULL
    ,[TaskListId] [bigint] NOT NULL
    ,[PersonId] [bigint] NOT NULL
    ,[AddUserId] [nvarchar](256) NOT NULL
    ,[AddUserName] [nvarchar](512) NOT NULL
    ,[AddDateTime] [datetime2](7) NOT NULL
    ,[UpdUserId] [nvarchar](256) NOT NULL
    ,[UpdUserName] [nvarchar](512) NOT NULL
    ,[UpdDateTime] [datetime2](7) NOT NULL
     CONSTRAINT [TaskAssign_PK] PRIMARY KEY CLUSTERED 
     (
         [TaskAssignId] ASC
     )
)
GO

CREATE TABLE [PrjMngSch].[TaskHier](
     [TaskHierId] [bigint] NOT NULL
    ,[ParentTaskListId] [bigint] NULL
    ,[SeqNum] [float] NOT NULL
    ,[TaskListId] [bigint] NOT NULL
    ,[TaskStatusId] [bigint] NOT NULL
    ,[AddUserId] [nvarchar](256) NOT NULL
    ,[AddUserName] [nvarchar](512) NOT NULL
    ,[AddDateTime] [datetime2](7) NOT NULL
    ,[UpdUserId] [nvarchar](256) NOT NULL
    ,[UpdUserName] [nvarchar](512) NOT NULL
    ,[UpdDateTime] [datetime2](7) NOT NULL
     CONSTRAINT [TaskHier_PK] PRIMARY KEY CLUSTERED 
     (
         [TaskHierId] ASC
     )
)
GO

CREATE TABLE [PrjMngSch].[TaskHours](
     [TaskHoursId] [bigint] IDENTITY(1,1) NOT NULL
    ,[TaskAssignId] [bigint] NOT NULL
    ,[StartDateTime] [datetime] NULL
    ,[FinishDateTime] [datetime] NULL
    ,[WorkHours] [float] NOT NULL
    ,[AddUserId] [nvarchar](256) NOT NULL
    ,[AddUserName] [nvarchar](512) NOT NULL
    ,[AddDateTime] [datetime2](7) NOT NULL
    ,[UpdUserId] [nvarchar](256) NOT NULL
    ,[UpdUserName] [nvarchar](512) NOT NULL
    ,[UpdDateTime] [datetime2](7) NOT NULL
 CONSTRAINT [TaskHours_PK] PRIMARY KEY CLUSTERED 
(
    ,[TaskHoursId] ASC
)
)
GO

CREATE TABLE [PrjMngSch].[TaskList](
     [TaskListId] [bigint] IDENTITY(1,1) NOT NULL
    ,[TaskListDesc] [nvarchar](512) NOT NULL
    ,[TaskListShortDesc] [nvarchar](50) NOT NULL
    ,[TaskTypeId] [bigint] NOT NULL
    ,[AddUserId] [nvarchar](256) NOT NULL
    ,[AddUserName] [nvarchar](512) NOT NULL
    ,[AddDateTime] [datetime2](7) NOT NULL
    ,[UpdUserId] [nvarchar](256) NOT NULL
    ,[UpdUserName] [nvarchar](512) NOT NULL
    ,[UpdDateTime] [datetime2](7) NOT NULL
 CONSTRAINT [TaskList_PK] PRIMARY KEY CLUSTERED 
(
    ,[TaskListId] ASC
)
)
GO

CREATE TABLE [PrjMngSch].[TaskStatus](
     [TaskStatusId] [bigint] NOT NULL
    ,[SeqNum] [float] NOT NULL
    ,[TaskStatusDesc] [nvarchar](50) NOT NULL
    ,[AddUserId] [nvarchar](256) NOT NULL
    ,[AddUserName] [nvarchar](512) NOT NULL
    ,[AddDateTime] [datetime2](7) NOT NULL
    ,[UpdUserId] [nvarchar](256) NOT NULL
    ,[UpdUserName] [nvarchar](512) NOT NULL
    ,[UpdDateTime] [datetime2](7) NOT NULL
 CONSTRAINT [TaskStatus_PK] PRIMARY KEY CLUSTERED 
(
     [TaskStatusId] ASC
)
)
GO

CREATE TABLE [PrjMngSch].[TaskType](
     [TaskTypeId] [bigint] NOT NULL
    ,[SeqNum] [float] NOT NULL
    ,[TaskTypeDesc] [nvarchar](100) NOT NULL
    ,[AddUserId] [nvarchar](256) NOT NULL
    ,[AddUserName] [nvarchar](512) NOT NULL
    ,[AddDateTime] [datetime2](7) NOT NULL
    ,[UpdUserId] [nvarchar](256) NOT NULL
    ,[UpdUserName] [nvarchar](512) NOT NULL
    ,[UpdDateTime] [datetime2](7) NOT NULL
 CONSTRAINT [TaskType_PK] PRIMARY KEY CLUSTERED 
(
    ,[TaskTypeId] ASC
)
)
GO

ALTER TABLE [PrjMngSch].[TaskAssign] ADD  CONSTRAINT [DF_TaskAssign_AddUserId]  DEFAULT ('') FOR [AddUserId]
GO

ALTER TABLE [PrjMngSch].[TaskAssign] ADD  CONSTRAINT [DF_TaskAssign_AddUserName]  DEFAULT (suser_name()) FOR [AddUserName]
GO

ALTER TABLE [PrjMngSch].[TaskAssign] ADD  CONSTRAINT [DF_TaskAssign_AddDateTime]  DEFAULT (sysdatetime()) FOR [AddDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskAssign] ADD  CONSTRAINT [DF_TaskAssign_UpdUserId]  DEFAULT ('') FOR [UpdUserId]
GO

ALTER TABLE [PrjMngSch].[TaskAssign] ADD  CONSTRAINT [DF_TaskAssign_UpdUserName]  DEFAULT (suser_name()) FOR [UpdUserName]
GO

ALTER TABLE [PrjMngSch].[TaskAssign] ADD  CONSTRAINT [DF_TaskAssign_UpdDateTime]  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskHier] ADD  CONSTRAINT [DF_TaskHier_AddUserId]  DEFAULT ('') FOR [AddUserId]
GO

ALTER TABLE [PrjMngSch].[TaskHier] ADD  CONSTRAINT [DF_TaskHier_AddUserName]  DEFAULT (suser_name()) FOR [AddUserName]
GO

ALTER TABLE [PrjMngSch].[TaskHier] ADD  CONSTRAINT [DF_TaskHier_AddDateTime]  DEFAULT (sysdatetime()) FOR [AddDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskHier] ADD  CONSTRAINT [DF_TaskHier_UpdUserId]  DEFAULT ('') FOR [UpdUserId]
GO

ALTER TABLE [PrjMngSch].[TaskHier] ADD  CONSTRAINT [DF_TaskHier_UpdUserName]  DEFAULT (suser_name()) FOR [UpdUserName]
GO

ALTER TABLE [PrjMngSch].[TaskHier] ADD  CONSTRAINT [DF_TaskHier_UpdDateTime]  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskHours] ADD  CONSTRAINT [DF_TaskHours_AddUserId]  DEFAULT ('') FOR [AddUserId]
GO

ALTER TABLE [PrjMngSch].[TaskHours] ADD  CONSTRAINT [DF_TaskHours_AddUserName]  DEFAULT (suser_name()) FOR [AddUserName]
GO

ALTER TABLE [PrjMngSch].[TaskHours] ADD  CONSTRAINT [DF_TaskHours_AddDateTime]  DEFAULT (sysdatetime()) FOR [AddDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskHours] ADD  CONSTRAINT [DF_TaskHours_UpdUserId]  DEFAULT ('') FOR [UpdUserId]
GO

ALTER TABLE [PrjMngSch].[TaskHours] ADD  CONSTRAINT [DF_TaskHours_UpdUserName]  DEFAULT (suser_name()) FOR [UpdUserName]
GO

ALTER TABLE [PrjMngSch].[TaskHours] ADD  CONSTRAINT [DF_TaskHours_UpdDateTime]  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskList] ADD  CONSTRAINT [DF_TaskList_AddUserId]  DEFAULT ('') FOR [AddUserId]
GO

ALTER TABLE [PrjMngSch].[TaskList] ADD  CONSTRAINT [DF_TaskList_AddUserName]  DEFAULT (suser_name()) FOR [AddUserName]
GO

ALTER TABLE [PrjMngSch].[TaskList] ADD  CONSTRAINT [DF_TaskList_AddDateTime]  DEFAULT (sysdatetime()) FOR [AddDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskList] ADD  CONSTRAINT [DF_TaskList_UpdUserId]  DEFAULT ('') FOR [UpdUserId]
GO

ALTER TABLE [PrjMngSch].[TaskList] ADD  CONSTRAINT [DF_TaskList_UpdUserName]  DEFAULT (suser_name()) FOR [UpdUserName]
GO

ALTER TABLE [PrjMngSch].[TaskList] ADD  CONSTRAINT [DF_TaskList_UpdDateTime]  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskStatus] ADD  CONSTRAINT [DF_TaskStatus_AddUserId]  DEFAULT ('') FOR [AddUserId]
GO

ALTER TABLE [PrjMngSch].[TaskStatus] ADD  CONSTRAINT [DF_TaskStatus_AddUserName]  DEFAULT (suser_name()) FOR [AddUserName]
GO

ALTER TABLE [PrjMngSch].[TaskStatus] ADD  CONSTRAINT [DF_TaskStatus_AddDateTime]  DEFAULT (sysdatetime()) FOR [AddDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskStatus] ADD  CONSTRAINT [DF_TaskStatus_UpdUserId]  DEFAULT ('') FOR [UpdUserId]
GO

ALTER TABLE [PrjMngSch].[TaskStatus] ADD  CONSTRAINT [DF_TaskStatus_UpdUserName]  DEFAULT (suser_name()) FOR [UpdUserName]
GO

ALTER TABLE [PrjMngSch].[TaskStatus] ADD  CONSTRAINT [DF_TaskStatus_UpdDateTime]  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskType] ADD  CONSTRAINT [DF_TaskType_AddUserId]  DEFAULT ('') FOR [AddUserId]
GO

ALTER TABLE [PrjMngSch].[TaskType] ADD  CONSTRAINT [DF_TaskType_AddUserName]  DEFAULT (suser_name()) FOR [AddUserName]
GO

ALTER TABLE [PrjMngSch].[TaskType] ADD  CONSTRAINT [DF_TaskType_AddDateTime]  DEFAULT (sysdatetime()) FOR [AddDateTime]
GO

ALTER TABLE [PrjMngSch].[TaskType] ADD  CONSTRAINT [DF_TaskType_UpdUserId]  DEFAULT ('') FOR [UpdUserId]
GO

ALTER TABLE [PrjMngSch].[TaskType] ADD  CONSTRAINT [DF_TaskType_UpdUserName]  DEFAULT (suser_name()) FOR [UpdUserName]
GO

ALTER TABLE [PrjMngSch].[TaskType] ADD  CONSTRAINT [DF_TaskType_UpdDateTime]  DEFAULT (sysdatetime()) FOR [UpdDateTime]
GO


