USE [RuleEngine]
GO

/****** Object:  Table [dbo].[FieldMetadata]    Script Date: 25-05-2026 11:24:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FieldMetadata](
	[FieldId] [int] IDENTITY(1,1) NOT NULL,
	[FieldName] [varchar](100) NULL,
	[DataType] [varchar](50) NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FieldMetadata] ADD  DEFAULT ((1)) FOR [IsActive]
GO


CREATE TABLE [dbo].[RuleAction](
	[ActionId] [int] IDENTITY(1,1) NOT NULL,
	[RuleId] [int] NULL,
	[ActionKey] [varchar](100) NULL,
	[ActionValue] [varchar](200) NULL,
	[DataType] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RuleAction]  WITH CHECK ADD FOREIGN KEY([RuleId])
REFERENCES [dbo].[RulesMaster] ([RuleId])


CREATE TABLE [dbo].[RuleCondition](
	[RuleConditionId] [int] IDENTITY(1,1) NOT NULL,
	[FieldId] [int] NOT NULL,
	[Operator] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RuleConditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RuleCondition]  WITH CHECK ADD FOREIGN KEY([FieldId])
REFERENCES [dbo].[FieldMetadata] ([FieldId])


CREATE TABLE [dbo].[RuleConditionGroup](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[RuleId] [int] NOT NULL,
	[GroupOrder] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RuleConditionGroup]  WITH CHECK ADD FOREIGN KEY([RuleId])
REFERENCES [dbo].[RulesMaster] ([RuleId])


CREATE TABLE [dbo].[RuleConditionMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[RuleId] [int] NULL,
	[RuleConditionValueId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RuleConditionMapping]  WITH CHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[RuleConditionGroup] ([GroupId])
GO

ALTER TABLE [dbo].[RuleConditionMapping]  WITH CHECK ADD  CONSTRAINT [FK_RuleConditionMapping_RuleConditionValue] FOREIGN KEY([RuleConditionValueId])
REFERENCES [dbo].[RuleConditionValue] ([RuleConditionValueId])
GO

ALTER TABLE [dbo].[RuleConditionMapping] CHECK CONSTRAINT [FK_RuleConditionMapping_RuleConditionValue]
GO

ALTER TABLE [dbo].[RuleConditionMapping]  WITH CHECK ADD  CONSTRAINT [FK_RuleConditionMapping_RulesMaster] FOREIGN KEY([RuleId])
REFERENCES [dbo].[RulesMaster] ([RuleId])
GO

ALTER TABLE [dbo].[RuleConditionMapping] CHECK CONSTRAINT [FK_RuleConditionMapping_RulesMaster]


CREATE TABLE [dbo].[RuleConditionValue](
	[RuleConditionValueId] [int] IDENTITY(1,1) NOT NULL,
	[RuleConditionId] [int] NOT NULL,
	[FieldValue] [varchar](200) NULL,
	[RuleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RuleConditionValueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RuleConditionValue]  WITH CHECK ADD FOREIGN KEY([RuleConditionId])
REFERENCES [dbo].[RuleCondition] ([RuleConditionId])
GO

ALTER TABLE [dbo].[RuleConditionValue]  WITH CHECK ADD  CONSTRAINT [FK_RuleConditionValue_RulesMaster] FOREIGN KEY([RuleId])
REFERENCES [dbo].[RulesMaster] ([RuleId])
GO

ALTER TABLE [dbo].[RuleConditionValue] CHECK CONSTRAINT [FK_RuleConditionValue_RulesMaster]

CREATE TABLE [dbo].[RuleGroupOperator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RuleId] [int] NOT NULL,
	[Operator] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RuleGroupOperator]  WITH CHECK ADD FOREIGN KEY([RuleId])
REFERENCES [dbo].[RulesMaster] ([RuleId])


CREATE TABLE [dbo].[RulesMaster](
	[RuleId] [int] IDENTITY(1,1) NOT NULL,
	[RuleName] [varchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UseCaseId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[RuleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RulesMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[RulesMaster] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[RulesMaster]  WITH CHECK ADD  CONSTRAINT [FK_RulesMaster_UseCases] FOREIGN KEY([UseCaseId])
REFERENCES [dbo].[UseCaseMaster] ([UseCaseId])
GO

ALTER TABLE [dbo].[RulesMaster] CHECK CONSTRAINT [FK_RulesMaster_UseCases]


CREATE TABLE [dbo].[UseCaseMaster](
	[UseCaseId] [int] IDENTITY(1,1) NOT NULL,
	[UseCaseCode] [varchar](100) NULL,
	[UseCaseName] [varchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UseCaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UseCaseCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UseCaseMaster] ADD  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[UseCaseMaster] ADD  DEFAULT (getdate()) FOR [CreatedDate]


CREATE TABLE [dbo].[UseCaseRuleMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UseCaseId] [int] NULL,
	[RuleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UseCaseRuleMapping]  WITH CHECK ADD FOREIGN KEY([RuleId])
REFERENCES [dbo].[RulesMaster] ([RuleId])
GO

ALTER TABLE [dbo].[UseCaseRuleMapping]  WITH CHECK ADD FOREIGN KEY([UseCaseId])
REFERENCES [dbo].[UseCaseMaster] ([UseCaseId])