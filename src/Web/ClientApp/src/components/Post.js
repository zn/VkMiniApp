import React, { Component } from 'react';
import { CardGrid, Card, Avatar, Cell, Div } from '@vkontakte/vkui';

class Post extends Component {

    format_date(givenDate){
		const timeString = givenDate.toLocaleTimeString('ru-RU', { timeStyle: 'short' });
		const currentDate = new Date();
		
		givenDate.setHours(0);
		givenDate.setMinutes(0);
		givenDate.setSeconds(0);
		currentDate.setHours(0);
		currentDate.setMinutes(0);
		currentDate.setSeconds(0);

		const msInDay = 8.64e+7; // 8.64e+7 is miliseconds in day
		if(currentDate - givenDate < msInDay && currentDate.getDate() === givenDate.getDate()){ 
			return `сегодня в ${timeString}`;
		}
		else if(currentDate - givenDate < msInDay*2){
			return `вчера в ${timeString}`;
		}
		return `${givenDate.toLocaleString('ru', {
						year: 'numeric',
						month: 'long',
						day: 'numeric'
					})} в ${timeString}`;
    }

    render() {
        const { post } = this.props;
        return (
			<CardGrid>
				<Card size="l" mode="shadow">
					<Cell before={<Avatar src={post.isAnonymously ? "" : post.author.photo100} />}
						description={this.format_date(new Date(post.publishDate))}>
						{post.isAnonymously ? "Аноним" : post.author.fullName}
					</Cell>
					<Div>
						{post.content}
					</Div>
				</Card>
			</CardGrid>
        )
    }
}

export default Post;