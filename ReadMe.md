<h2>Help Instruction Service Stack:</h2>

<h3>Help Instruction API Information</h3>
<p>The help instruction API is a simple web API that a consumer can interact with via Http RESTful requests.</p>
<p>The API supports functionality for the following operations:
	<ul>
		<li>Read with paging, filters and sorts</li>
		<li>Read for a specifi host and key</li>
		<li>Creation of new help instructions</li>
		<li>Updates to existing help instructions</li>
		<li>Deletion of an existing help instruction</li>
	</ul>
</p>
<p>The Swagger OpenAPI 2.0 specification is also adhered to and swagger documention discoverable.</p>
<p>An API reference to this API is required by both the component and admin website.</p>
<p><b>Requires Asp.Net Core 2.1</b></p>

<h3>Help Instruction Web Information</h3>
<p>The help instruction Website can be used to manage help instructions for use by consumed help instruction components as well as provide contact and usage instructions for consumers:</p>
<p><b>Requires Asp.Net Core 2.1</b></p>
<p><b>To build this project a NuGet source feed must be setup as follows: Telerik (https://nuget.telerik.com/nuget)</b></p>
<p><b>Valid Telerik credentials must be supplied and saved - see NugGet.Config in Web project for further details.</b></p>

<h3>Help Instruction Component Information</h3>
<p>
    The help instruction tooltip component is a simple reusable instruction popup that reads tooltip data from the help instruction API for a specific key.
    This data can be read either at load time or on demand as the help instruction is invoked (by hovering over the help icon).
    This on demand loading means that the Html footprint for the help instruction component is minimised and the tooltip data only loaded when required.
</p>
<p>
    The help instruction tooltip data can be managed through the admin web portal, users can add edit and delete any help instructions for any given host and lookup key.
    When a help instruction component is used on a page (for a specific host/key) the component will use this key data to retrieve the correct tooltip.
    Note that after a component initialises its tooltip data (either in pre-fetch or on-demand) for the duration of the page life cycle that data is cached and no subsequent calls will be made until the page is refreshed or reloaded.
</p>
<p>
    The help instruction tooltip component can be utilised on any Asp Net Core web site simple by adding the component reference from the official Nuget feed;
</p>

<ul>
    <li><b>From Package-Manager</b>: Install-Package Andgasm.HelpInstruction.Web.Component -Version 0.0.1</li>
    <li><b>From DotNet CLI</b>: dotnet add package Andgasm.HelpInstruction.Web.Component --version 0.0.1</li>
    <li><b>From Nuget Visual Studio GUI</b>: Search for Andgasm.HelpInstruction.Web.Component</li>
</ul>

<p>
    Details of the published help instruction component package can be found here:
</p>

<p>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="https://www.nuget.org/packages/Andgasm.HelpInstruction.Web.Component/">https://www.nuget.org/packages/Andgasm.HelpInstruction.Web.Component/</a>
</p>
<p>
    Note that any use of a help instruction component on a web site will require a path to the root of a deployed instance of the Help Instruction API instance
</p>
<p><b>Requires DotNet Standard 2.0</b></p>

<h3>Help Instruction UITests Usage Instructions</h3>
<p>
    To execute UI tests the executing machine must have performed the following steps before first execution:
	<ul>
		<li>Install testing components: <a href="https://marketplace.visualstudio.com/items?itemName=AtinBansal.SeleniumcomponentsforCodedUICrossBrowserTesting">Selenium Drivers</a></li>
		<li>Ensure Chrome Driver is latest: <a href="http://chromedriver.storage.googleapis.com/2.42/chromedriver_win32.zip">Chrome Driver (latest as of doc date)</a> &amp; extract executable to: C:\Program Files (x86)\Common Files\microsoft shared\VSTT\Cross Browser Selenium Components\</li>
	</ul>
</p>

<h3>Help Instruction Component Usage Instructions</h3>
<p>
    The help instruction component can be invoked in a few different ways depending on the consumers development preferances;
</p>
<ul>
    <li>Tag Helper</li>
    <li>ViewComponent Invoke</li>
    <li>HelpInstruction Render Extensions</li>
</ul>

<h4>Tag Helper Usage Example</h4>
<p>
    To place a help instruction component on a view using the tag helpers use the following markup on your view at the location you wish the help icon to appear;
</p>
<pre>
    @addTagHelper *, Andgasm.HelpInstruction.Web.Component
    ...
    ...
    <vc:help-instruction apirooturl="https://localhost:44300" ondemand="true" hostkey="testhost" datakey="testkey"
                         label="test label" suppressscripts="false" suppressstyles="false"></vc:help-instruction>
</pre>

<p>
    The above indicates that a help instruction component should be rendered on the page in position. The component will look to the specified
    api url using both the specified host key and specified data to retrieve the correct tooltip data. As the component has been told to load
    on-demand the call for the tooltip data will not happen until the user rolls over the help instruction component element, at which point the
    data will be retrieved and the tooltip will be shown until the user hovers away.
</p>
<p>
    Note that the flags for suppressing scripts and styles are set to
    false which means the full help component markup will be renderd with script and style refs. If these were set to true then the component would
    render only it's mark up and not the dependency links which can be loaded seperatly in a page header or footer, this is covered in the Render Extensions section.
</p>
<p>
    Also note that there is a known limitation with TagHelpers where optional parameters must be specified in the markup.
</p>

<h4>Razor Invoke Usage Example</h4>
<p>
    To place a help instruction component on a view using the razor syntax use the following markup on your view at the location you wish the help icon to appear;
</p>
<pre>
    @await Component.InvokeAsync("HelpInstruction", new { apirooturl = "https://localhost:44300" 
                                                          ondemand = "true" hostkey = "testhost" datakey = "testkey"
                                                          label = "test label" suppressscripts = "false" suppressstyles = "false" })
</pre>
<p>
    As with the TagHelper example, the above will render in the same way, the only difference being how it was invoked in the view. Note that a consuming page can invoke 
    any number of components in a mixture of ways.
</p><br />

<h4>Render Extensions Usage Example</h4>

TODO!