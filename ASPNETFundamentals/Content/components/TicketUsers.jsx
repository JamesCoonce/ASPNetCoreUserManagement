import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import Select from 'react-select';

class TicketUsers extends React.Component {

    constructor(props) {
        super(props);
        this.onChange = this.onChange.bind(this);
        this.switchToMulti = this.switchToMulti.bind(this);
        this.switchToSingle = this.switchToSingle.bind(this);
        this.getUsers = this.getUsers.bind(this);
        this.gotoUser = this.gotoUser.bind(this);
        this.toggleBackspaceRemoves = this.toggleBackspaceRemoves.bind(this);
        this.toggleCreatable = this.toggleCreatable.bind(this);
        this.alertUsers = this.alertUsers.bind(this);
        this.state = { backspaceRemoves: true, multi: true};
    }
    onChange(value) {
        this.setState({
            value: value,
        });
    }
    switchToMulti() {
        this.setState({
            multi: true,
            value: [this.state.value],
        });
    }
    switchToSingle() {
        this.setState({
            multi: false,
            value: this.state.value ? this.state.value[0] : null
        });
    }
    getUsers(input) {
        if (!input) {
            return Promise.resolve({ options: [] });
        }

        return fetch(`http://localhost:27184/api/TicketsAPI/users?username=${input}`)
            .then((response) => response.json())
            .then((json) => {
                return { options: json };
            });
    }
    gotoUser(value, event) {
        //window.open(value.html_url);
        alert(value.email);
    }

    removeUser(value, event) {
        alert("Remove " + value.email);
    }
    toggleBackspaceRemoves() {
        this.setState({
            backspaceRemoves: !this.state.backspaceRemoves
        });
    }
    toggleCreatable() {
        this.setState({
            creatable: !this.state.creatable
        });
    }


    alertUsers(event) {
        var users = this.state.value;
        alert(users);
        console.log(users);
        console.log(JSON.stringify(users));
        event.preventDefault();
    }
    

    render() {
        const AsyncComponent = this.state.creatable
            ? Select.AsyncCreatable
            : Select.Async;

        return (
            <div className="section">
                <h3 className="section-heading">{this.props.label}</h3>
                <AsyncComponent multi={this.state.multi} value={this.state.value} onChange={this.onChange} onValueClick={this.gotoUser} valueKey="id" labelKey="userName" loadOptions={this.getUsers} backspaceRemoves={this.state.backspaceRemoves} />
                <div className="checkbox-list">
                    <label className="checkbox">
                        <input type="radio" className="checkbox-control" checked={this.state.multi} onChange={this.switchToMulti} />
                        <span className="checkbox-label">Multiselect</span>
                    </label>
                    <label className="checkbox">
                        <input type="radio" className="checkbox-control" checked={!this.state.multi} onChange={this.switchToSingle} />
                        <span className="checkbox-label">Single Value</span>
                    </label>
                </div>
                <div className="checkbox-list">
                    <label className="checkbox">
                        <input type="checkbox" className="checkbox-control" checked={this.state.creatable} onChange={this.toggleCreatable} />
                        <span className="checkbox-label">Creatable?</span>
                    </label>
                    <label className="checkbox">
                        <input type="checkbox" className="checkbox-control" checked={this.state.backspaceRemoves} onChange={this.toggleBackspaceRemoves} />
                        <span className="checkbox-label">Backspace Removes?</span>
                    </label>
                </div>
                <div className="hint">This example uses fetch.js for showing Async options with Promises</div>
                <button className="btn btn-success" onClick={this.alertUsers}>Alert Users </button>
            </div>
        );
    }
}

//ReactDOM.render(<TicketUsers />, document.getElementById('ticket-users'));
//ReactDOM.render(<TicketUsers />);
export default TicketUsers;