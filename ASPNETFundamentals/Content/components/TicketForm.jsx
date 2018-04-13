import React, { Component } from 'react';
import { Button, FormGroup, ControlLabel, FormControl, FieldGroup } from 'react-bootstrap';

class TicketForm extends Component {
    constructor(props) {
        super(props)
        this.handleSubmit = this.handleSubmit.bind(this);
        this.state = {
            priorities: [
                {
                    "id": 0,
                    "text": "High Impact"
                },

                {
                    "id": 1,
                    "text": "Medium Impact"
                },
                {
                    "id": 2,
                    "text": "Low Impact"
                }
            ],
            resolutions: [
                {
                    "id": 0,
                    "text": "UnAssigned"
                },
                {
                    "id": 1,
                    "text": "Assigned"
                },

                {
                    "id": 2,
                    "text": "Testing"
                },
                {
                    "id": 3,
                    "text": "Completed"
                },
                {
                    "id": 4,
                    "text": "Rejected"
                }
            ]
        }
    }

    handleSubmit(event) {
        alert("hello");
        event.preventDefault();

    }
    render() {
        return (
            <form onSubmit={this.handleSubmit}>

                
                <FormGroup controlId="descriptionControl">
                    <ControlLabel>Description</ControlLabel>
                    <FormControl componentClass="textarea" placeholder="Description" />
                </FormGroup>

                <FormGroup controlId="prioritylevel">
                    <ControlLabel>Priority Level</ControlLabel>
                    <FormControl componentClass="select" placeholder="select">
                        {this.state.priorities.map(priority =>
                            <option key={priority.id} value={priority.id}>{priority.text}</option>
                            )}
                    </FormControl>
                </FormGroup>

                <FormGroup controlId="resolution">
                    <ControlLabel>Resolution</ControlLabel>
                    <FormControl componentClass="select" placeholder="select">
                        {this.state.resolutions.map(resolution =>
                            <option key={resolution.id} value={resolution.id}>{resolution.text}</option>
                            )}
                    </FormControl>
                </FormGroup>

                <Button type="submit" onClick={this.handleSubmit}>
                    Button
                </Button>
            </form>
        )
    }
}

export default TicketForm;
//TicketForm.propTypes = {
//    currentTicket: React.PropTypes.object.isRequired,
//    handleInputChange: react.PropTypes.func.isRequired,
//    handleSubmit: React.PropTypes.func.isRequired
//}