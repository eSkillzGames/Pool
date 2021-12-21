
import React from 'react';
import { Button, FormControlLabel, Checkbox, makeStyles, LinearProgress } from '@material-ui/core';
import styles from './style';

const useStyles = makeStyles(styles);

const Cue = (props) => {
  const classes = useStyles();

  const { name, level, exp, currentPower, totalPower, image, force, aim, spin, time, isActive, price, isAuto, isSelected, ...rest } = props;

  return (
    <div className={`${classes.cue} ${isSelected ? classes.selected_cue : ''}`} {...rest}>
      <div>
        <div>
          <h3>{name}</h3>
          <div>
            <p>{level} Level</p>
            <LinearProgress variant="determinate" value={exp} />
            <p>{currentPower}/{totalPower}</p>
            <img src="/images/power.png" alt="" />
          </div>
        </div>
        <img src={"/images/" + image} alt="" />
      </div>
      <div>
        <Power power={force} label="Force" />
        <Power power={aim} label="Aim" />
        <Power power={spin} label="Spin" />
        <Power power={time} label="Time" />
      </div>
      <div>
        {isActive ? (
          <>
            <Button id="upgrade">Upgrade Cue</Button>
            <div>
              <Button>
                <img src="/images/refresh.png" alt="" />
                <img src="/images/token.png" alt="" />
                <span>{price}</span>
              </Button>
              <FormControlLabel control={<Checkbox checked={isAuto} style={{color: isAuto ? 'white' : 'grey'}} />} label="Auto" />
            </div>
          </>
        ) : (
          <Button id="buy">
            Buy
            <img src="/images/token.png" alt="" />
            <span>{price}</span>
          </Button>
        )}
      </div>
      { isSelected && <label>Selected</label> }
    </div>
  )
}

const Power = (props) => {
  const classes = useStyles();
  const { power, label } = props;

  return (
    <div className={classes.power}>
      {[...Array(10)].map((_, index) => (
        <div key={index} id={ power > index ? 'active' : '' } />
      ))}
      <p>{label}</p>
    </div>
  )
}

const Cues = () => {
  const classes = useStyles();

  const data = [
    {
      name: 'Revolutionary',
      level: 2,
      exp: 80,
      currentPower: 10,
      totalPower: 20,
      image: 'cue1.png',
      force: 4,
      aim: 2,
      spin: 3,
      time: 1,
      isActive: true,
      price: 50,
      isAuto: true,
    },
    {
      name: 'Scalable',
      level: 2,
      exp: 20,
      currentPower: 20,
      totalPower: 20,
      image: 'cue2.png',
      force: 4,
      aim: 2,
      spin: 3,
      time: 1,
      isActive: true,
      price: 60,
      isAuto: false,
    },
    {
      name: 'Bricks-and-clicks',
      level: 3,
      exp: 50,
      currentPower: 25,
      totalPower: 25,
      image: 'cue1.png',
      force: 4,
      aim: 2,
      spin: 5,
      time: 3,
      isActive: false,
      price: 500,
    },
    {
      name: 'E-business',
      level: 4,
      exp: 90,
      currentPower: 30,
      totalPower: 30,
      image: 'cue2.png',
      force: 4,
      aim: 2,
      spin: 6,
      time: 3,
      isActive: false,
      price: 1000,
    },
  ];

  const [selected, setSelected] = React.useState(0);
  
  return (
    <>
      <div className={classes.hero}>
        {data.map((cue, index) => (
          <Cue 
            key={index}
            name={cue.name}
            level={cue.level}
            exp={cue.exp}
            currentPower={cue.currentPower}
            totalPower={cue.totalPower}
            image={cue.image}
            force={cue.force}
            aim={cue.aim}
            spin={cue.spin}
            time={cue.time}
            isActive={cue.isActive}
            price={cue.price}
            isAuto={cue.isAuto}
            isSelected={selected === index}
            onClick={() => setSelected(index)}
          />
        ))}
      </div>
    </>
  );
}

export default Cues;