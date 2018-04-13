import React, { Component } from 'react';
import { BrowserRouter as Router, Route, Link, browserHistory } from 'react-router-dom';
import TicketUsers from './TicketUsers';
import { loadTickets, fetchTicket, deleteTicket } from './ticketService';
import TicketForm from './TicketForm';


class App extends Component {

    constructor(props) {
        super(props);
        this.state = {
            todos: []
        }
    }

    componentDidMount() {
        loadTickets()
            .then(todos => this.setState({ todos }))
    }

    componentWillReceiveProps(nextProps) {
        console.log(nextProps)
        loadTickets()
            .then(todos => this.setState({ todos }))
    }

    render() {
        return (
            <Router history={browserHistory}>
                <div>
                    <ul>
                        <li><Link to="/">Home</Link></li>
                        <li><Link to="/tickets">Tickets</Link></li>
                    {this.state.todos.map(todo =>
                        <li key={todo.id}>

                                <Link to={`/todos/${todo.id}`}>
                                    {todo.title}
                                </Link>
                        </li>
                    )}
                   </ul>
                
                <Route exact path="/" component={Home} />
                <Route path="/tickets" component={TicketUsers} />
                <Route path="/todos/:id" component={Todo} />
                </div>
            </Router>
        )
    }
}

const Home = () => (
    <div>
        <h2>Home</h2>
    </div>
)


class Todo extends Component {
    constructor(props) {
        super(props);
        this.state = {
            todo: []
        }
        this.deleteTicketItem = this.deleteTicketItem.bind(this)
    }

    componentDidMount() {
        fetchTicket(this.props.match.params.id)
            .then(todo => this.setState({todo}))
    }

    componentWillReceiveProps(nextProps) {
        fetchTicket(nextProps.match.params.id)
            .then(todo => this.setState({ todo }));
        console.log(nextProps);
    }

    deleteTicketItem() {
        deleteTicket(this.state.todo.id)
            .then(console.log('Delete ' + this.state.todo.id))
        
    }
    render() {
        return (
            <div>
                <h3>The id is {this.state.todo.id}</h3>
                <h3>The title is {this.state.todo.title}</h3>
                <h3>The description is {this.state.todo.description}</h3>
                <button className="btn btn-danger" onClick={this.deleteTicketItem}>Delete</button>
                <TicketForm />
            </div>
        )
    }
}

class TodoDelete extends Component {

}


export default App;