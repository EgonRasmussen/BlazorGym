# Two-way binding i Blazor

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

### **ParentCounter.razor**

```csharp
@page "/parentcounter"

<h3>ParentCounter = @currentCount</h3>

<ChildCounter @bind-CurrentCount=currentCount IncrementAmount="incrementAmount" />

@code {
    int currentCount = 0;
    int incrementAmount = 10;
}
```

#### **Forklaring**:
- **`@bind-CurrentCount`**:
  - Binder `currentCount` i forælderen til `CurrentCount` i barnet.
  - Binder også opdateringer fra barnet til forælderen. Dette er essensen af **two-way binding** i Blazor.
  
- **`IncrementAmount`**:
  - Sender værdien af `incrementAmount` (10) til `ChildCounter`, som bruges til at styre, hvor meget tælleren forøges med.

---

### **Two-way binding: Hvordan det fungerer**

1. Når du klikker på knappen i `ChildCounter`:
   - `CurrentCount` øges med værdien af `IncrementAmount`.
   - Den nye værdi sendes tilbage til forælderen ved hjælp af `CurrentCountChanged.InvokeAsync(CurrentCount)`.

2. I `ParentCounter`:
   - `@bind-CurrentCount` sørger for, at `currentCount` opdateres automatisk med den nye værdi fra barnet.

3. Resultatet:
   - `ParentCounter` og `ChildCounter` holder begge styr på den samme værdi (`currentCount`) og opdateres automatisk i begge retninger.

---

### Visualisering af dataflow:
1. **Forælder til barn**:
   - Initialiser `CurrentCount` og `IncrementAmount` i barnet.
   
2. **Barn til forælder**:
   - Når `ChildCounter` opdaterer `CurrentCount`, sendes den nye værdi tilbage til `ParentCounter`.

