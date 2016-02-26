ALTER TABLE LoadType
ADD CONSTRAINT FK_LoadType_Status
FOREIGN KEY (StatusId)
REFERENCES [dbo].Status(value)