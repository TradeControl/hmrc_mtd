# HMRC MTD

The tax algorithms of Trade Control calculate VAT on a transaction-grained basis and present real time results in a [Vat Statement](https://tradecontrol.github.io/tutorials/balance-sheet-web#vat). The objective is to integrate UK government digital tax services with the statement.

All UK Vat registered businesses must now submit their Vat Returns through the HMRC MTD service. This development will therefore proceed in two stages:

1. Allow any UK business to post their VAT Return anonymously for free.
2. Deliver an API for Trade Control users to post their VAT from within the app.  

## Technology

The app is written in ASP.NET Core and SQL Server. 

[MTD API Documentation](https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0)

## Development Plan

### Phase 1 MTD API Interface

- [x] HMRC OAuth
- [x] Sandbox VAT API calls
- [x] Fraud-prevention headers 

### Phase 2 - Environment

- [ ] Entity Framework Core scaffold
- [ ] Asynchronous connection to the database  
- [ ] Authentication
- [ ] Register new users 
- [ ] Authorisation
- [ ] [Device Detection](https://github.com/wangkanai/Detection)
- [ ] Layouts and Navigation
- [ ] Session service


### Phase 3 - Web App

- [ ] Run [VatMTDController.cs](https://github.com/TradeControl/hmrc_mtd/blob/master/mtd-client-vat/Controllers/VatMTDController.cs) inside a user session
- [ ] Liabilities
- [ ] Submit Returns
- [ ] Past Returns

### Phase 4 - API

- [ ] Implement in Azure
- [ ] Call from the [Trade Control Web App](https://github.com/TradeControl/tradecontrol.web)

## Donations

[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=C55YGUTBJ4N36)

## Licence

The Trade Control Code licence is issued by Trade Control Ltd under a [GNU General Public Licence v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html) 
