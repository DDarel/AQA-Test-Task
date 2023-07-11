Feature: Demoblaze testing 

Scenario: Sign up on demoblaze
Given User open home page
When User signing up
| UserName | Password |
| Admin | admin123 |
Then User succesfully signed up

Scenario: Sign up on demoblaze with invalid data
Given User open home page
When User signing up
| UserName | Password |
| Admin | admin123 |
Then User didn't sign up

Scenario: Add item to a cart
Given User open Samsung galaxy s6 item page
When User click on Add to cart button
Then Item successfully added

Scenario: Delete item from a cart
Given User added Samsung galaxy s6 to cart
And User open cart page
When User click on delete Samsung galaxy s6 button
Then Samsung galaxy s6 successfully deleted

Scenario: Place an order
Given User added Samsung galaxy s6 to cart
And User open cart page
When User click on Place order button
And User fill order info
| Name | Country | City | CreditCard | Month | Year |
| Vlad | Ukraine   | Kyiv | 0000           | 01         | 2023 |
Then Order successfully placed
