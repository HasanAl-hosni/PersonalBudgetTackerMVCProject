async function loadSummary() {
    const response = await fetch('/api/summaryapi');
    if (response.ok) {
        const data = await response.json();
        document.getElementById('incomeVal').innerText = '+' + data.income.toFixed(2) + ' $';
        document.getElementById('expenseVal').innerText = '-' + data.expense.toFixed(2) + ' $';
        document.getElementById('balanceVal').innerText = data.balance.toFixed(2) + ' $';
    } else {
        console.error("API call failed");
    }
}

loadSummary();
