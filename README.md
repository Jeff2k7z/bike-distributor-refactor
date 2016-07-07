# Bike Distributor Refactor
This is my solution to the Bike Distributor Refactoring exercise.  I set a time box for this exercise at 2 hours, but found myself saying "one more thing" several times.  I ended up putting about 4 hours of total work into it.  There is still so much more that could be done!

### Multiple Product Types
The solution now supports adding more than just bikes to an order.  By decoupling the order line from bikes class and creating a more generic Product class, you can now easily add bikes, chamois cream, tubes, gels, etc. to the order.

### Promotion Engine
Now you can create as many promotions as you'd like without changing the Order code. By separating the promotion logic out of the invoice rendering method, and by creating a Promotion class, we can perform complex order calculations independently of invoice generation.  Promotions can target the line level as well as the order level and can be set to activate for a range of prices a range of quantities or both.  

### Receipt/Invoice Templates
The code now contains a simple template engine that performs basic tag replacements to generate invoices.  The engine supports both line level as well as order level templates.  Although the current engine is fairly basic, it is capable of generating quite complex invoices as you can see by the example below.  This allows adding new or changing existing templates without changing code.  

### Preview
![Preview](http://i.imgur.com/OfkCdc5.png)

Enjoy!