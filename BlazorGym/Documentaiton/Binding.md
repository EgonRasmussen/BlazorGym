# Binding

## One-way binding
Vi lægger ud med default Counter eksemplet. Her flyder data fra komponenten og ud i UI'et. Der bindes til en lokal variabel `currentCount`.

```csharp
@page "/counter"

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```
Der foregår også en One-way binding den modsatte vej, nemlig når knappen klikkes og `IncrementCount` metoden opdaterer `currentCount`, 
som så automatisk opdaterer UI'et.

Der kan også sendes event argumenter med, se [ASP.NET Core Blazor event handling](https://learn.microsoft.com/da-dk/aspnet/core/blazor/components/event-handling?view=aspnetcore-10.0).

Der kan også benyttes en Conditional Attribute, her vist med `disabled` attributten. En one-liner metoder kan også laves med lambda udtryk:
```csharp
<button class="btn btn-primary"
        disabled="@(currentCount >= 10)"
        @onclick="() => currentCount++">
  Click me
</button>
```

&nbsp;

## Two-Way Data Binding
Vises med en `<input>`:
```csharp
<p><input type="number" @bind-value="@increment" /></p>     @* Ny *@

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;
    private int increment = 1;      // Ny

    private void IncrementCount()
    {
        currentCount += increment;
    }
}
```

Default Event for `<input>` er `onchange`, hvilket betyder at værdien først opdateres når fokus forlader inputfeltet.
Der kan også bindes til andre Events, f.eks. `@bind-value:event="oninput"`, der opdaterer for hver ændring:

```csharp
<input type="number" @bind-value="@increment" @bind-value:event="oninput" />
```

### Formatting Dates
```csharp
<p>
  <input @bind="@Today" @bind:format="yyyy-MM-dd" />
</p>
@Today.ToLongDateString()
@code {
  private DateTime Today { get; set; } = DateTime.Now;
}
```

&nbsp;

# Parent–Child Communication

**ParentCounter.razor:**

```csharp
@page "/parentcounter"

<h3>ParentCounter = @currentCount</h3>

 <ChildCounter @bind-CurrentCount=currentCount IncrementAmount="incrementAmount" />

@code {
    int currentCount = 0;
    int incrementAmount = 10;
}
```

**ChildCounter.razor:**
```csharp
 <h5>ChildCounter = @CurrentCount</h5>
 <button class="btn btn-primary" @onclick="IncrementCount">Increment</button>

@code {
    [Parameter]
    public int CurrentCount { get; set; }

    [Parameter]
    public int IncrementAmount { get; set; }

    [Parameter]
    public EventCallback<int> CurrentCountChanged { get; set; }

    private async Task IncrementCount()
    {
        CurrentCount += IncrementAmount;
        await CurrentCountChanged.InvokeAsync(CurrentCount);
    }
}
 ```

#### **Forklaring**:
- **[Parameter]:** 
  - `CurrentCount`: Bruges til at modtage den aktuelle tællerværdi fra en overordnet komponent.
  - `IncrementAmount`: Modtager et tal fra den overordnede komponent, som angiver, hvor meget tælleren skal forøges med.
  - `CurrentCountChanged`: Gør det muligt at sende opdateringer tilbage til den overordnede komponent.
  
- **Two-way binding**:
  - Når knappen klikkes, opdaterer `IncrementCount` metoden `CurrentCount` og sender den nye værdi tilbage til den overordnede komponent med `CurrentCountChanged.InvokeAsync(CurrentCount)`.

Fordele ved EventCallback:
- EventCallback benytter structs, hvilket betyder at vi ikke behøver foretage en null-check.
- EventCallback er asyncron og kan awaites.
- Blazor eksekverer automatisk StateHasChanged() på den overordnede komponent, når EventCallback kaldes.
  Dette sikrer at UI opdateres korrekt.























  --- 

  ## Nu med Parameters:
Nu oprettes to Parameters, som dog ikke ændrer noget (endnu):

```csharp
@page "/counter"

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @CurrentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    [Parameter]
    public int IncrementAmount { get; set; } = 1;
    [Parameter]
    public int CurrentCount { get; set; } = 0;

    private void IncrementCount()
    {
        CurrentCount++;
    }
}
```

&nbsp;

## Opdeling i to componenter
Det begynder at blive smart når vi opdeler i to componenter:

**ParentCounter.razor:**

```csharp
@page "/parentcounter"

<h3>ParentCounter = @currentCount</h3>

<ChildCounter CurrentCount=currentCount IncrementAmount="incrementAmount" />

@code {
    int currentCount = 0;
    int incrementAmount = 10;
}
```

**ChildCounter.razor:**
```csharp
    <h5>ChildCounter = @CurrentCount</h5>
    <button class="btn btn-primary" @onclick="IncrementCount">Increment</button>
</div>

@code {
    [Parameter]
    public int CurrentCount { get; set; }

    [Parameter]
    public int IncrementAmount { get; set; }

    private void IncrementCount()
    {
        CurrentCount += IncrementAmount;
    }
}
```

Men desværre opdateres ParentCounter ikke når ChildCounter opdateres. Dette kan løses med Two-way binding:


&nbsp;