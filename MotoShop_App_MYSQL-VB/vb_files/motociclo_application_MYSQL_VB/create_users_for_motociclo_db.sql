-- =====================================================
-- CREATE USERS FOR MOTOCICLO_DB
-- =====================================================
-- This script creates the users needed for the VB.NET application
-- to work with the existing motociclo_db database
-- =====================================================

-- =====================================================
-- 1. Create Login User (for account_page and account_signup)
-- =====================================================

CREATE USER IF NOT EXISTS 'vb_login'@'localhost' IDENTIFIED BY 'login123';

-- Grant permissions for login operations
GRANT SELECT, INSERT ON motociclo_db.accounts TO 'vb_login'@'localhost';

-- =====================================================
-- 2. Create Employee User
-- =====================================================

CREATE USER IF NOT EXISTS 'mc_vbemployee'@'localhost' IDENTIFIED BY 'emp123';

-- Grant permissions for employee operations
GRANT SELECT, INSERT, UPDATE, DELETE ON motociclo_db.* TO 'mc_vbemployee'@'localhost';

-- =====================================================
-- 3. Create Customer User
-- =====================================================

CREATE USER IF NOT EXISTS 'mc_vbcustomer'@'localhost' IDENTIFIED BY 'cust123';

-- Grant permissions for customer operations
GRANT SELECT, INSERT, UPDATE, DELETE ON motociclo_db.* TO 'mc_vbcustomer'@'localhost';

-- =====================================================
-- 4. Ensure Root User Has Access
-- =====================================================

-- Root user should already have all privileges, but let's make sure
GRANT ALL PRIVILEGES ON motociclo_db.* TO 'root'@'localhost';

-- =====================================================
-- 5. Flush Privileges
-- =====================================================

FLUSH PRIVILEGES;

-- =====================================================
-- 6. Verify Users Created
-- =====================================================

-- Show all users
SELECT User, Host FROM mysql.user WHERE User IN ('vb_login', 'mc_vbemployee', 'mc_vbcustomer', 'root');

-- Show privileges for each user
SHOW GRANTS FOR 'vb_login'@'localhost';
SHOW GRANTS FOR 'mc_vbemployee'@'localhost';
SHOW GRANTS FOR 'mc_vbcustomer'@'localhost';
SHOW GRANTS FOR 'root'@'localhost';

-- =====================================================
-- USERS CREATED SUCCESSFULLY!
-- =====================================================
-- Your VB.NET application can now use the existing connection strings
-- with the motociclo_db database
-- ===================================================== 