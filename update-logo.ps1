# Script to force update the logo file
Write-Host "Removing logo from git cache..."
git rm --cached DreamFlowLabs/wwwroot/images/dreamflow-logo.png

Write-Host "Adding logo file..."
git add -f DreamFlowLabs/wwwroot/images/dreamflow-logo.png

Write-Host "Checking status..."
git status DreamFlowLabs/wwwroot/images/dreamflow-logo.png

Write-Host "If the file shows as modified, commit it with:"
Write-Host "git commit -m 'Update logo to new design'"
Write-Host "git push origin main"

