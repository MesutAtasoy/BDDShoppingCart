Feature: ShoppingCart
Shopping cart operations

    @Shopping-Cart
    Scenario: Add a product randomly to an empty shopping cart
        Given an empty shopping cart
        When I add a product randomly to the shopping cart.
        Then The price of the shopping cart is a valid number.
        And the added item should be in the shopping cart

    Scenario: Add products with quantity and product code to an empty shopping cart
        Given an empty shopping cart
        When I add 1 item(s) with the product code "Computer01" to my shopping cart.
        Then The shopping cart costs 1100

    Scenario: Add multiple products with quantity and product code to an empty shopping cart
        Given an empty shopping cart
        When I add 1 item(s) with the product code "Computer01" to my shopping cart.
        And I add 2 item(s) with the product code "Keyboard01" to my shopping cart.
        Then The shopping cart costs 1200