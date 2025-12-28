Security Policy
Important Security Context
Risk n Reward is an educational programming project that:

Uses virtual currency only (VMali)

Has no real money transactions

Contains no financial or payment systems

Is not connected to any banking or payment APIs

Serves as a portfolio and learning project

Despite this limited scope, we take security seriously for the protection of our contributors and users.

Supported Versions
Only the latest major version receives security updates:

Version	Supported
1.x.x	Yes
< 1.0	No
Reporting a Vulnerability
If you discover a security issue, please DO NOT open a public issue.

Instead, please email the maintainer directly at:
bmap750@gmail.com

What to Include in Your Report
Description of the vulnerability

Steps to reproduce (if applicable)

Potential impact assessment

Suggested fix (optional but appreciated)

Your contact information (for follow-up questions)

Response Timeline
Initial Response: Within 48 hours

Assessment: Within 7 days

Fix Timeline: Depends on severity (typically 1-4 weeks)

Public Disclosure: After fix is available

Security Considerations for This Project
Areas of Concern
While this project doesn't handle sensitive data, these areas should be secure:

Random Number Generation

Must use cryptographically secure random generators if implemented

Should be properly seeded

Input Validation (for future web components)

Console input sanitation

API parameter validation (when implemented)

Dependency Security

Regular updates of the .NET Framework

Monitoring for vulnerable NuGet packages

What's NOT a Security Concern
Virtual currency manipulation - VMali has no real value

Game outcome prediction - All games are for demonstration only

Score/highscore manipulation - No competitive rankings

Development Security Guidelines
For Contributors
Never introduce real payment systems

Keep all currency virtual (use VMali only)

Validate all user inputs in console/web interfaces

Use secure random generators for game outcomes

Avoid storing any personal data

Code Review Focus Areas
During pull request reviews, we specifically check:

No hardcoded secrets or API keys

Proper input validation

Safe random number generation

No real-world currency references

Appropriate error handling

Dependency Security
We maintain security through:

Regular Updates: Keeping .NET and dependencies current

Automated Scanning: Using GitHub's Dependabot (when enabled)

Minimal Dependencies: Only essential packages are used

Security Update Process
Assessment: Evaluate vulnerability impact

Private Fix: Develop a fix in a private branch if needed

Testing: Verify fix doesn't break functionality

Release: Deploy fix in next patch/minor version

Credit: Acknowledge reporter (if desired)

Disclosure: Update SECURITY.md with details

Security Education
As an educational project, we encourage:

Learning about secure coding practices

Understanding probability and random number security

Exploring game mechanics without real-world risk

Contact
For security concerns: bmap750@gmail.com
For general questions: Use GitHub Discussions
