USE [bazaRadova];
SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

SET IDENTITY_INSERT [dbo].[Rad] ON;

BEGIN TRANSACTION;
INSERT INTO [dbo].[Rad]([ID], [Name], [Year], [filePDFpath], [fileTXTpath])
SELECT 1, N'Automatic protocol', 2014, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\Automatic protocol.pdf', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\Automatic protocol.txt' UNION ALL
SELECT 2, N'From input private', 2011, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\From input private.pdf', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\From input private.txt' UNION ALL
SELECT 3, N'From input private', 2011, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\2From input private.pdf', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\2From input private.txt' UNION ALL
SELECT 4, N'Verifiable computation', 2014, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\Verifiable computation.pdf', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\Verifiable computation.txt' UNION ALL
SELECT 5, N'ChipWhisperer', 2013, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\ChipWhisperer.pdf', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\ChipWhisperer.txt' UNION ALL
SELECT 6, N'Unified Oblivious-RAM', 2012, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\Unified Oblivious-RAM.pdf', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\Unified Oblivious-RAM.txt' UNION ALL
SELECT 7, N'asdsadsadsad', 2014, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\asdsadsadsad', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\asdsadsadsad.txt' UNION ALL
SELECT 8, N'saddsfghjklkjhgfdsgfhjkljhgfd', 2014, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\saddsfghjklkjhgfdsgfhjkljhgfd', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\saddsfghjklkjhgfdsgfhjkljhgfd.txt' UNION ALL
SELECT 9, N'aaasadasdasdasdasd', 2014, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\aaasadasdasdasdasd', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\aaasadasdasdasdasd.txt' UNION ALL
SELECT 10, N'aaaaaaaaaaaaaaaaaaaaa1', 2014, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\aaaaaaaaaaaaaaaaaaaaa1', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\aaaaaaaaaaaaaaaaaaaaa1.txt' UNION ALL
SELECT 11, N'zzzzzzzzzzzzzzzzzzzzz', 2011, N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\zzzzzzzzzzzzzzzzzzzzz', N'C:\Users\Dusan\Desktop\PlagijatorFinder\aspFileUploadStoreTag\uploadFiles\zzzzzzzzzzzzzzzzzzzzz.txt' UNION ALL
SELECT 12, N'bla', 2010, N'C:\Users\Dusan\Desktop\PlagijatorFinder\PlagijatorFinder\uploadFiles\bla', N'C:\Users\Dusan\Desktop\PlagijatorFinder\PlagijatorFinder\uploadFiles\bla.txt'
COMMIT;
RAISERROR (N'[dbo].[Rad]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;
GO

SET IDENTITY_INSERT [dbo].[Rad] OFF;

