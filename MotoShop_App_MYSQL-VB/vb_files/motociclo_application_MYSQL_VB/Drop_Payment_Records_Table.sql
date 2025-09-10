-- Drop Payment_Records table and its indexes
-- This script removes the redundant Payment_Records table since payment information
-- is now stored directly in the Orders table

-- Drop indexes first
DROP INDEX IF EXISTS idx_payment_records_order_id ON Payment_Records;
DROP INDEX IF EXISTS idx_payment_records_date ON Payment_Records;

-- Drop the Payment_Records table
DROP TABLE IF EXISTS Payment_Records;

-- Verify the table is dropped
SELECT 'Payment_Records table has been successfully dropped.' AS Status; 