import React, { Component } from 'react';
import PropTypes from 'prop-types';
import Typography from '@material-ui/core/Typography';
import { withStyles } from '@material-ui/core/styles';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import OutlinedInput from '@material-ui/core/OutlinedInput';
import InputLabel from '@material-ui/core/InputLabel';
import FormControl from '@material-ui/core/FormControl';
import ReactDOM from 'react-dom';
import Switch from '@material-ui/core/Switch';
import TextField from '@material-ui/core/TextField';

const styles = theme => ({
    root: {
        width: '100%',
        maxWidth: 500,
        marginTop: 84,
        marginLeft: 40
    },
    formControl: {
        margin: theme.spacing.unit,
        minWidth: 150,
        marginLeft: -2
    },
    selectEmpty: {
        marginTop: theme.spacing.unit * 2
    }
});

export class Dashboard extends Component {

    constructor(props) {
        super(props);
        this.state = {
            dashboardData: {},
            isLoading: false
        };
    }

    componentDidMount() {
        this.getDashboardData();
    }

    handleSettingsChange = event => {

    };

    handleSwitchChange = event => {

    };

    render() {
        let temperature = this.state.dashboardData.temperature
            ? this.state.dashboardData.temperature.toLocaleString(undefined, { maximumFractionDigits: 2 })
            : 0;

        let humidity = this.state.dashboardData.humidity
            ? this.state.dashboardData.humidity.toLocaleString(undefined, { maximumFractionDigits: 2 })
            : 0;

        if (this.state.hasErrored) {
            return <p>Sorry! There was an error loading the items</p>;
        }

        if (this.state.isLoading) {
            return <p>Loading…</p>;
        }

        return (
            <div className="root">
                <Typography variant="h6" gutterBottom>
                    Dashboard
                </Typography>
                <Typography variant="body1" gutterBottom>
                    <div className="Dashboard">
                        <div>Sensors &amp; Relays</div>
                        <div>Temperature:&nbsp;{temperature}&nbsp;&#8451;</div>
                        <div>Humidity:&nbsp;{humidity}&nbsp;%</div>
                        <div>Is Relay On:&nbsp;{String(this.state.dashboardData.isOn)}</div>
                        <br />
                    </div>
                </Typography>
                <FormControl variant="outlined" className="formControl">
                    <InputLabel
                        ref={ref => {
                            this.InputLabelRef = ref;
                        }}
                        htmlFor="outlined-mode-simple">
                        Mode
                    </InputLabel>
                    <Select
                        value={this.state.dashboardData.mode}
                        onChange={this.handleSettingsChange}
                        input={< OutlinedInput labelWidth="labelWidth"
                            name="mode"
                            id="outlined-mode-simple" />}>
                        <MenuItem value={0}>Manual</MenuItem>
                        <MenuItem value={1}>Automatic</MenuItem>
                    </Select>
                </FormControl>
                {
                    this.state.dashboardData.mode === 0 ?
                        <div>
                            <div>Toggle relay</div>
                            <Switch
                                name="isOn"
                                checked={this.state.dashboardData.isOn}
                                onChange={this.handleSwitchChange}
                            />
                        </div>
                        : null
                }
                {
                    this.state.dashboardData.mode === 1 ?
                        <div>
                            <TextField
                                id="outlined-number"
                                name="innerTemperatureMin"
                                label="Indoor temperature minimum"
                                value={this.state.dashboardData.innerTemperatureMin}
                                type="number"
                                className="textField"
                                InputLabelProps={{
                                    shrink: true,
                                }}
                                margin="normal"
                                variant="outlined"
                                onChange={this.handleSettingsChange}
                            />
                        </div>
                        : null
                }

            </div>
        );
    }

    async getDashboardData() {
        const response = await fetch('api/Dashboard');
        const data = await response.json();
        this.setState({ dashboardData: data, loading: false });
    }
}

