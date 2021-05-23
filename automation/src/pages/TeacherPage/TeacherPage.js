import React, { Component } from 'react'
import { STATE_LOGGED_IN, STATE_LOGGED_OUT } from '../../App';
import { Redirect } from 'react-router';
export default class TeacherPage extends Component {
    constructor(props){
        super(props);
        this.state = {
        }
    }
    render() {
        if (this.props.loggedIn != 1) {
            return <Redirect to="/login" />;
        }
        return (
            <div>
                Teacher
            </div>
        )
    }
}
