# One-way binding i Blazor
Vi lægger ud med default Counter eksemplet:

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

&nbsp;

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

# Two-way binding i Blazor

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

    private void IncrementCount()
    {
        CurrentCount += IncrementAmount;
        CurrentCountChanged.InvokeAsync(CurrentCount);
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

---



