import React, {Component} from 'react';
import MeasuresChart from '../MeasureChart/MeasuresChart';
import {withStyles} from '@material-ui/core/styles';

const styles = {
  root: {
    width: '100%',
    maxWidth: 800,
    marginTop: 84,
    marginLeft: 40
  }
};

class Measures extends Component {

  render() {
    const {classes} = this.props;
    return (
      <div className={classes.root}>
        <MeasuresChart></MeasuresChart>
      </div>
    )
  }
}
Measures.propTypes = {}

export default withStyles(styles)(Measures);