ALTER TABLE Source
ADD CONSTRAINT FK_Source_Status
FOREIGN KEY (StatusId)
REFERENCES [dbo].Status(value)