
truncate table journal_entry;

ALTER TABLE [dbo].[journal_entry] DROP [fk_batch_id]
GO

truncate table entry_batch;

ALTER TABLE [dbo].[journal_entry]  WITH CHECK ADD  CONSTRAINT [fk_batch_id] FOREIGN KEY([batch_id])
REFERENCES [dbo].[entry_batch] ([id])
GO

ALTER TABLE [dbo].[journal_entry] CHECK CONSTRAINT [fk_batch_id]
GO

ALTER TABLE [dbo].[journal_entry]  DROP [fk_master_location] 
GO

truncate table master_location;

ALTER TABLE [dbo].[journal_entry]  WITH CHECK ADD  CONSTRAINT [fk_master_location] FOREIGN KEY([location_code])
REFERENCES [dbo].[master_location] ([code])
GO

ALTER TABLE [dbo].[journal_entry] CHECK CONSTRAINT [fk_master_location]
GO



ALTER TABLE [dbo].[master_location]  DROP [fk_branch_code]
GO

truncate table master_branch;

ALTER TABLE [dbo].[master_location]  WITH CHECK ADD  CONSTRAINT [fk_branch_code] FOREIGN KEY([branch_code])
REFERENCES [dbo].[master_branch] ([code])
GO

ALTER TABLE [dbo].[master_location] CHECK CONSTRAINT [fk_branch_code]
GO


ALTER TABLE [dbo].[journal_entry] DROP [fk_master_partner]
GO

ALTER TABLE [dbo].[master_location] DROP [fk_partner_code]
GO

truncate table master_partner;


ALTER TABLE [dbo].[journal_entry]  WITH CHECK ADD  CONSTRAINT [fk_master_partner] FOREIGN KEY([partner_code])
REFERENCES [dbo].[master_partner] ([code])
GO

ALTER TABLE [dbo].[journal_entry] CHECK CONSTRAINT [fk_master_partner]
GO


ALTER TABLE [dbo].[master_location]  WITH CHECK ADD  CONSTRAINT [fk_partner_code] FOREIGN KEY([partner_code])
REFERENCES [dbo].[master_partner] ([code])
GO

ALTER TABLE [dbo].[master_location] CHECK CONSTRAINT [fk_partner_code]
GO



