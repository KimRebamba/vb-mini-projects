-- Add user_id column to employees table
-- This makes the relationship between employees and accounts optional
ALTER TABLE employees 
ADD COLUMN user_id INT NULL;

-- Add foreign key constraint to reference accounts table
ALTER TABLE employees 
ADD CONSTRAINT fk_employees_user_id 
FOREIGN KEY (user_id) REFERENCES accounts(user_id) 
ON DELETE SET NULL 
ON UPDATE CASCADE;

-- Add index for better performance on user_id lookups
CREATE INDEX idx_employees_user_id ON employees(user_id); 