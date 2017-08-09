USE [bazaRadova];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[Autor] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Autor]([IDAutor], [ImeAutora])
SELECT 1, N'glalgal' UNION ALL
SELECT 2, N'dsfsdfsdf' UNION ALL
SELECT 3, N'gfg' UNION ALL
SELECT 4, N'fghfhg' UNION ALL
SELECT 5, N'bla'
COMMIT;
RAISERROR (N'[dbo].[Autor]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Autor] OFF;

