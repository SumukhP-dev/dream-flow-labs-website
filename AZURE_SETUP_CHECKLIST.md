# Azure Federated Identity Setup Checklist

## Current Error

```
AADSTS700213: No matching federated identity record found for presented assertion subject 'repo:SumukhP-dev/dream-flow-labs-website:ref:refs/heads/main'
```

## Required Configuration

### ✅ Step 1: Verify GitHub Secrets

Go to: `https://github.com/SumukhP-dev/dream-flow-labs-website/settings/secrets/actions`

**Check these secrets exist with EXACT values:**

1. **Secret Name**: `AZURE_CLIENT_ID`

   - **Value**: `c474d109-0619-40be-bc18-c798af85fe05`
   - ❌ Common mistake: Extra spaces, wrong format

2. **Secret Name**: `AZURE_TENANT_ID`

   - **Value**: `067983d3-432e-4806-8c0b-8cb52dedfe17`
   - ❌ Common mistake: Wrong tenant ID

3. **Secret Name**: `AZURE_SUBSCRIPTION_ID`
   - **Value**: Your subscription ID (check in Azure Portal)

**Action**: If any secret is missing or incorrect, update it now.

---

### ✅ Step 2: Verify Azure Federated Identity Credential

1. Go to [Azure Portal](https://portal.azure.com)
2. Navigate to: **Azure Active Directory** → **App registrations**
3. Search for: `DreamflowLabsMain` or Client ID: `c474d109-0619-40be-bc18-c798af85fe05`
4. Click on the app registration
5. Go to: **Certificates & secrets** → **Federated credentials** tab

**Check if a credential exists. If it doesn't exist or is incorrect, follow these steps:**

#### Create/Update Federated Credential:

1. Click **Add credential** (or delete existing and create new)
2. Select: **GitHub Actions deploying Azure resources**
3. Fill in **EXACTLY** (case-sensitive):
   - **Organization**: `SumukhP-dev`
   - **Repository**: `dream-flow-labs-website`
   - **Entity type**: `Branch`
   - **Branch name**: `main`
   - **Name**: `dreamflow-main-branch` (or any name)
4. Click **Add**

**Critical**: The subject identifier must be EXACTLY:

```
repo:SumukhP-dev/dream-flow-labs-website:ref:refs/heads/main
```

**Common Mistakes:**

- ❌ `Sumukhp-dev` (wrong case)
- ❌ `Dream-Flow-Labs-Website` (wrong case)
- ❌ `Main` or `MAIN` (should be lowercase `main`)
- ❌ Extra spaces anywhere

---

### ✅ Step 3: Verify Role Assignment

1. Go to your **Subscription** in Azure Portal
2. Navigate to: **Access control (IAM)**
3. Click: **Role assignments** tab
4. Search for: `DreamflowLabsMain` or `c474d109-0619-40be-bc18-c798af85fe05`

**Check**: The app registration should have **Contributor** role (or appropriate permissions)

**If missing:**

1. Click **Add** → **Add role assignment**
2. Role: **Contributor**
3. Assign access to: `DreamflowLabsMain` (search by name or Client ID)
4. Click **Review + assign**

---

### ✅ Step 4: Wait for Propagation

After creating/updating the federated credential:

- ⏱️ Wait **1-2 minutes** for Azure AD to propagate changes
- Then re-run the workflow

---

### ✅ Step 5: Test the Workflow

1. Go to: `https://github.com/SumukhP-dev/dream-flow-labs-website/actions`
2. Click: **Build and deploy ASP.Net Core app to Azure Web App - dreamflow-ai**
3. Click: **Run workflow** → **Run workflow**

---

## Quick Verification Commands (if you have Azure CLI)

If you have Azure CLI installed, you can verify the federated credential:

```bash
az login
az ad app show --id c474d109-0619-40be-bc18-c798af85fe05 --query "federatedIdentityCredentials"
```

---

## Still Not Working?

If you've verified all the above and it still fails:

1. **Double-check the exact subject** in Azure matches:

   ```
   repo:SumukhP-dev/dream-flow-labs-website:ref:refs/heads/main
   ```

2. **Delete and recreate** the federated credential (sometimes helps with propagation issues)

3. **Verify you're in the correct Azure AD tenant**:

   - Check your profile icon (top right) in Azure Portal
   - Tenant ID should be: `067983d3-432e-4806-8c0b-8cb52dedfe17`

4. **Check for typos** in the repository name - it's `dream-flow-labs-website` (with hyphens, lowercase)
