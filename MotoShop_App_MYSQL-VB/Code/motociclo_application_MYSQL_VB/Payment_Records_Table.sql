-- Add payment_amount and payment_change columns to Orders table if they don't exist
ALTER TABLE Orders ADD COLUMN IF NOT EXISTS payment_amount DECIMAL(10,2) NULL;
ALTER TABLE Orders ADD COLUMN IF NOT EXISTS payment_change DECIMAL(10,2) NULL;

-- Drop Payment_Records table if it exists (since payment info is now in Orders table)
DROP TABLE IF EXISTS Payment_Records; 