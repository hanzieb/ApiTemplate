# ApiTemplate (A Portfolio proof-of-concept and API primer)

<h2>A working showcase-of-skills for portfolio use showcasing many real-world concepts and use cases for a Web API Core implementation in .Net 9.</h2>

<p>This version is still a work in progress, and the API related functions are coming soon. Feel free to use or critique any of the code here. I plan to use it as a launching platform for future web api implementations but its a good starting foundation with some mock data as well.</p>

<img width="500" height="500" alt="sample site image" src="https://github.com/user-attachments/assets/a38de88d-beae-4fdf-b8d4-28c89c2fd5ff" />
<img width="500" height="500" alt="sample site image" src="https://github.com/user-attachments/assets/fbe02688-08e4-483d-94cb-1415d8b0401e" />


<h3>This version showcases:</h3>
<p>
<ul>
<li>fully separated MVC model with data, services, and front end</li>
<li>Entity framework core running fully in ram-only mode using static connection pooling and a custom instance serving factory container</li>
<li>Unit-Of-Work/Repository/App patterns</li>
<li>fully async/await and parallel process ready, including the db store via multiple context executions</li>
<li>fully pathed cancellation token implementations</li>
<li>static, server-side rendered gallery page in ASP.Net</li>
<li>Unit Tests</li>
</ul>
</p>

<h3>Future Updates:</h3>
<p>
<ul>
<li>Integration Tests</li>
<li>Gallery json payload function</li>
<li>convert gallery ui page to client side rendering using an api ajax call</li>
<li>anonymous bearer token issuance via CAPTCHA solving</li>
<li>ODATA filtering and searching functions for gallery items</li>
<li>CRUD functions for gallery items linked to session-stored dbms instead of global ram</li>
</ul>
</p>

<p>Feel free to use or fork my project if it helps in any way. Just please leave me a mention somewhere. Cheers =)</p>

<h1>Release Notes:</h1>

<h3>07/13/25</h3>
<p>Initial release</p>

<h3>07/15/25</h3>
<p>Added Unit Tests and sample images to readme</p>

<h3>Using these 3rd party resources:</h3>
<ul>
    <li><a href="https://www.flaticon.com/free-icons/shiba-inu" title="shiba inu icons">Shiba inu icons created by AomAm - Flaticon</a></li>
    <li><a href="https://getbootstrap.com/" title="Bootstrap">Theming and layouts by Bootstrap</a></li>
    <li><a href="https://feimosi.github.io/baguetteBox.js/" title="Baguettebox.js">Pure Js lighbox by baguetteBox.js</a></li>
</ul>
