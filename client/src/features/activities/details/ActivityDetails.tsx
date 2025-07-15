import { Card, Button, CardActions, CardMedia, CardContent, Typography } from '@mui/material'

type Props = {
  activity : Activity;
}

export default function ActivityDetails({activity} : Props) {
  return (
    <Card sx={{borderRadius: 3}}>
      <CardMedia component='img' alt="Image" src={`/images/categoryImages/drinks.jpg`}
      />
      <CardContent>
        <Typography variant="h5">{activity.title}</Typography>
        <Typography variant="subtitle1" fontWeight='light'>{activity.date}</Typography>
        <Typography variant="body1">{activity.description}</Typography>
      </CardContent>
      <CardActions>
        <Button color='primary'>Edit</Button>
        <Button color='inherit'>Cancel</Button>
      </CardActions>
    </Card>
  )
}
