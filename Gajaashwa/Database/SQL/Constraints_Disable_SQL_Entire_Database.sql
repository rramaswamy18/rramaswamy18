-- Disable all constraints in a database
EXEC sp_msforeachtable "ALTER TABLE ? NOCHECK CONSTRAINT all"
