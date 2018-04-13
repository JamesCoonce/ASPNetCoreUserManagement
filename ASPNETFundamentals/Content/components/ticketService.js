const baseUrl = "http://localhost:27184/api/TicketsAPI";

export const loadTickets = () => {
    return fetch(baseUrl)
        .then(res => res.json())
}

export const fetchTicket = (ticketId) => {
    return fetch(`${baseUrl}/${ticketId}`)
     .then(res => res.json())
}

export const createTicket = (ticket) => {
    return fetch(`${baseUrl}/${ticket.id}`, {
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ticket)
    }).then(res => res.json())}

export const updateTicket = (ticket) => {
    return fetch(`${baseUrl}/${ticketId}`, {
        method: 'put',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(ticket)
    }).then(res => res.json())
}

export const deleteTicket = (ticketId) => {
    return fetch(`${baseUrl}/${ticketId}`, {
        method: 'delete',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
}